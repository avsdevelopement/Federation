using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public class ClsAVS5111
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string Sql = "", Stage = "";
    string sResult = "";
    int Result = 0;

    public ClsAVS5111()
    {

    }

    public string getbrname(string BrCode)
    {
        Sql = "select midname from BANKNAME where brcd='" + BrCode + "'";
        sResult = conn.sExecuteScalar(Sql);
        return sResult;
    }

    public string GetAccType(string AccT, string BrCode)
    {
        try
        {
            Sql = "SELECT GLNAME FROM GLMAST WHERE BRCD = '" + BrCode + "' AND GLCODE IN (2, 15) And SUBGLCODE  ='" + AccT + "'";
            sResult = conn.sExecuteScalar(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetAllData(string BrCode, string PrCode, string Month, string Year)
    {
        try
        {
            Sql = "Exec ISP_AVS0144 @Brcd = '" + BrCode + "', @AgentCode = '" + PrCode + "', @Month = '" + Month + "', @Year = '" + Year + "'";
            DT = conn.GetDatatable(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public void DeletePassBook(string BrCode, string PrCode, string month, string year)
    {
        try
        {
            Sql = "Delete From AVS5078 Where BrCd = '" + BrCode + "' And SubGlCode ='" + PrCode + "' And Month ='" + month + "' And Year = '" + year + "'";
            Result = conn.sExecuteQuery(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public int InsertPassBook(string BrCode, string PrCode, string year, string month, string srno, string accno, string name, string opening,
        string credit, string amount, string diff, string mid, string Status)
    {
        try
        {
            if (Status == "1")
                Stage = "1003";
            else
                Stage = "1001";

            Sql = "Insert Into AVS5078(SrNo, BrCd, SubGlCode, AccNo, CustName, OpeningBal, Credit, Amount, Diff, Month, Year, Stage, Mid, Status, SystemDate) " +
                  "Values('" + srno + "', '" + BrCode + "', '" + PrCode + "', '" + accno + "', '" + name + "', '" + opening + "', '" + credit + "', '" + amount + "', " +
                  "'" + diff + "', '" + month + "', '" + year + "', '" + mid + "', " + Stage + ", '" + Status + "', GetDate())";
            Result = conn.sExecuteQuery(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

   

}