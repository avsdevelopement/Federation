using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsCashDenom
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult = "", TableName = "";
    double Amount = 0;
    int Result = 0, i =0;
    DataTable DT = new DataTable();

	public ClsCashDenom()
	{
		
	}

    public DataTable BindVaultData(string mid,string BRCD)
    {
        try
        {
            DT = new DataTable();

            sql = "SELECT NOTE_TYPE, NO_OF_NOTES FROM AVS5011 WHERE MID = '" + mid + "' and BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string CheckUsrGrp(string BrCd, string UGrp)//Dhanya Shetty 04/08/2017 added usergroup=5
    {
        try
        {
            sql = "SELECT USERGROUP FROM USERMASTER WHERE BRCD = '" + BrCd + "' and LoginCode = '" + UGrp + "' AND USERGROUP=5";
            UGrp = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return UGrp = "0";
        }
        return UGrp;
    }

    public int InsertDataIn(string brcd, string subgl,string accno, string MID, string EDate, string setno, string tThousand, string thousand, string fhundred, string thundred, string hundred, string fifty, string twenty, string ten, string five, string two, string one, string coins)
    {
        try
        {
            string sql = "EXEC SP_CASHDENOM @BRCD = '" + brcd + "',@subgl='" + subgl + "',@DENSACC='" + accno + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @SETNO = '" + setno + "', @TWOTHOU = '" + tThousand + "', @THOUSAND = '" + thousand + "', @FIVEHUND = '" + fhundred + "', @TWOHUND = '" + thundred + "', @HUNDRED = '" + hundred + "', @FIFTY = '" + fifty + "', @TWENTY = '" + twenty + "', @TEN = '" + ten + "', @FIVE = '" + five + "', @TWO = '" + two + "', @ONE = '" + one + "', @COINS = '" + coins + "', @MID = '" + MID + "', @PCNAME = '" + conn.PCNAME().ToString() + "', @TYPE = 'REC'";
            i = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return i = 0;
        }
        return i;
    }

    public int InsertDataOut(string brcd, string subgl, string accno, string MID, string EDate, string setno, string tThousand, string thousand, string fhundred, string thundred, string hundred, string fifty, string twenty, string ten, string five, string two, string one, string coins)
    {
        try
        {
            sql = "EXEC SP_CASHDENOM @BRCD = '" + brcd + "',@subgl='" + subgl + "',@DENSACC='" + accno + "', @EDATE = '" + conn.ConvertDate(EDate).ToString() + "', @SETNO = '" + setno + "', @TWOTHOU = '" + tThousand + "', @THOUSAND = '" + thousand + "', @FIVEHUND = '" + fhundred + "', @TWOHUND = '" + thundred + "', @HUNDRED = '" + hundred + "', @FIFTY = '" + fifty + "', @TWENTY = '" + twenty + "', @TEN = '" + ten + "', @FIVE = '" + five + "', @TWO = '" + two + "', @ONE = '" + one + "', @COINS = '" + coins + "', @MID = '" + MID + "', @PCNAME = '" + conn.PCNAME().ToString() + "', @TYPE = 'PAY'";
            i = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return i = 0;
        }
        return i;
    }

    public string CheckCashSet(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD;

            TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + "" + TD[1].ToString();

            sql = "Select Top 1 Activity From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' And Activity = '3'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return sResult = "0";
        }
        return sResult;
    }

    public double GetVoucherAmount(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD;

            TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + "" + TD[1].ToString();

            sql = "Select Sum(Amount) From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' And TrxType = 1 And PmtMode Not In ('TR_INT','ABB-TR_INT')";
            Amount = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Amount = 0;
        }
        return Amount;
    }

    public DataTable CashDenom(string fbc, string tbc, string fd)
    {
        try
        {
            sql = "Exec SP_RPTCASHDENOM @FBRCD='" + fbc + "',@TBRCD='" + tbc + "',@EDATE ='" + conn.ConvertDate(fd) + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}