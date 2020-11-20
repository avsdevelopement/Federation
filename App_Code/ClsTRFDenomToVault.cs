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

public class ClsTRFDenomToVault
{
    DbConnection DBconn = new DbConnection();
    string sql = "", sQuery = "";
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsTRFDenomToVault()
	{
		
	}

    public DataTable BindVaultData(string BRCD)//BRCD ADDED --Abhishek
    {
        try
        {
            sql = "SELECT V_TYPE,NOTE_TYPE,NO_OF_NOTES FROM avs5011 WHERE V_TYPE=999 and BRCD='" + BRCD + "' And Stage = '1003' ORDER BY NOTE_TYPE DESC";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable BindStationData(string BRCD, string LogCode, string MID)
    {
        try
        {
            sql = "SELECT V_TYPE,NOTE_TYPE,NO_OF_NOTES FROM avs5011 WHERE BRCD = '" + BRCD + "' AND MID = (SELECT PERMISSIONNO FROM USERMASTER WHERE BRCD = '" + BRCD + "' and LOGINCODE = '" + LogCode + "') And Stage = '1003' ORDER BY NOTE_TYPE DESC";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetData(string brcd, string one, string two, string five, string ten, string twenty, string fifty, string hundred, string twohundred, string fhundred, string thousand, string tThousand, string coins, string SoiledNts, string EDate, string MID, string FrVault, string ToVault)
    {
        try
        {
            Result = DBconn.sExecuteQuery("EXEC SP_BALFROMTOVAULT @BRCD= '" + brcd + "', @ONE= '" + one + "', @TWO= '" + two + "', @FIVE= '" + five + "', @TEN= '" + ten + "', @TWENTY= '" + twenty + "', @FIFTY= '" + fifty + "', @HUNDRED= '" + hundred + "', @THUNDRED = '" + twohundred + "', @FHUNDRED= '" + fhundred + "', @THOUSAND= '" + thousand + "', @TTHOUSAND= '" + tThousand + "', @CHILLER= '" + coins + "', @SOILEDNTS= '" + SoiledNts + "', @FRVAULT = '" + FrVault + "', @TOVAULT = '" + ToVault + "', @EDate= '" + DBconn.ConvertDate(EDate).ToString() + "', @MID= '" + MID + "', @PCNAME= '" + DBconn.PCNAME().ToString() + "', @TYPE='T'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertData(string brcd, string one, string two, string five, string ten, string twenty, string fifty, string hundred, string twohundred, string fhundred, string thousand, string tThousand, string coins, string SoiledNts, string EDate, string MID, string FrVault, string ToVault)
    {
        try
        {
            Result = DBconn.sExecuteQuery("EXEC SP_BALFROMTOVAULT @BRCD= '" + brcd + "', @ONE= '" + one + "', @TWO= '" + two + "', @FIVE= '" + five + "', @TEN= '" + ten + "', @TWENTY= '" + twenty + "', @FIFTY= '" + fifty + "', @HUNDRED= '" + hundred + "', @THUNDRED = '" + twohundred + "', @FHUNDRED= '" + fhundred + "', @THOUSAND= '" + thousand + "', @TTHOUSAND= '" + tThousand + "', @CHILLER= '" + coins + "', @SOILEDNTS= '" + SoiledNts + "', @FRVAULT = '" + FrVault + "', @TOVAULT = '" + ToVault + "', @EDate= '" + DBconn.ConvertDate(EDate).ToString() + "', @MID= '" + MID + "', @PCNAME= '" + DBconn.PCNAME().ToString() + "', @TYPE='G'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string CheckUsrGrp(string BrCd, string LgCode)
    {
        try
        {
            sql = "SELECT USERGROUP FROM USERMASTER WHERE BRCD = '" + BrCd + "' and LOGINCODE = '" + LgCode + "'";
            LgCode = DBconn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return LgCode = "0";
        }
        return LgCode;
    }

    public string Getstage(string BRCD, string VType)
    {
        try
        {
            BRCD = DBconn.sExecuteScalar("SELECT TOP 1 V_TYPE FROM AVS5011 WHERE BRCD = '" + BRCD + "' AND V_TYPE = '" + VType + "' And Stage = '1003'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return BRCD = null;
        }
        return BRCD;
    }

    public string CashType(string BRCD, string CashierType)
    {
        try
        {
            sql = "SELECT TYPE FROM AVS5010 WHERE BRCD= '" + BRCD + "' AND USERCODE='" + CashierType + "' And Stage = '1003'";
            sQuery = DBconn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return sQuery = null;
        }
        return sQuery;
    }
}