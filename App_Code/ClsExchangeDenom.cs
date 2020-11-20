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

public class ClsExchangeDenom
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsExchangeDenom()
	{
		
	}

    public DataTable BindBrVaultData(string BRCD)
    {
        try
        {
            sql = "SELECT ID,ONENTS,TWONTS,FIVENTS,TENNTS,TWENTYNTS,FIFTYNTS,HUNDNTS,FHUNDNTS,ONETNTS,TWOTNTS,CHILLER,TOTAL,FROMVAULT FROM AVS5014 WHERE BRCD='" + BRCD + "'";//BRCD ADDED --Abhishek
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int InsertDataFromBVault(string id,string brcd, string one, string two, string five, string ten, string twenty, string fifty, string hundred, string fhundred, string thousand, string tThousand, string coins, string TotalAmt, string EDate, string MID)
    {
        int i;
        try
        {
            i = DBconn.sExecuteQuery("EXEC SP_BALFROMBRCHVAULT @BRCD= '" + brcd + "', @ONE= '" + one + "', @TWO= '" + two + "', @FIVE= '" + five + "', @TEN= '" + ten + "', @TWENTY= '" + twenty + "', @FIFTY= '" + fifty + "', @HUNDRED= '" + hundred + "', @FHUNDRED= '" + fhundred + "', @THOUSAND= '" + thousand + "', @TTHOUSAND= '" + tThousand + "', @CHILLER= '" + coins + "', @TOTAMOUNT= '" + TotalAmt + "', @EDate= '" + DBconn.ConvertDate(EDate).ToString() + "', @MID= '" + MID + "', @PCNAME= '" + DBconn.PCNAME().ToString() + "'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return i = 0;
        }
        return i;
    }

    public int InsertDataFromCashier(string id, string brcd, string one, string two, string five, string ten, string twenty, string fifty, string hundred, string fhundred, string thousand, string tThousand, string coins, string TotalAmt, string EDate, string MID)
    {
        int i;
        try
        {
            i = DBconn.sExecuteQuery("EXEC SP_BALTOBRCHVAULT @BRCD= '" + brcd + "', @ONE= '" + one + "', @TWO= '" + two + "', @FIVE= '" + five + "', @TEN= '" + ten + "', @TWENTY= '" + twenty + "', @FIFTY= '" + fifty + "', @HUNDRED= '" + hundred + "', @FHUNDRED= '" + fhundred + "', @THOUSAND= '" + thousand + "', @TTHOUSAND= '" + tThousand + "', @CHILLER= '" + coins + "', @TOTAMOUNT= '" + TotalAmt + "', @EDate= '" + DBconn.ConvertDate(EDate).ToString() + "', @MID= '" + MID + "', @PCNAME= '" + DBconn.PCNAME().ToString() + "'");
        }
        catch (Exception Ex)
        {
            return i = 0;
        }
        return i;
    }

    public string CheckUsrGrp(string BrCd, string UGrp)
    {
        try
        {
            sql = "SELECT USERGROUP FROM USERMASTER WHERE BRCD = '" + BrCd + "' and LOGINCODE = '" + UGrp + "'";
            UGrp = DBconn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return UGrp = "0";
        }
        return UGrp;
    }
}