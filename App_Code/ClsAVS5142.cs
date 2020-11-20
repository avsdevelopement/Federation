using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5142
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;
    double ClBal = 0;

    public ClsAVS5142()
    {

    }

    public int GetVoucherInfo(GridView Gview, string BrCode, string EDate, string FSetNo, string TSetNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            string TBNAME = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select A.BrCd, ConVert(VarChar(10), A.EntryDate, 103) As EntryDate, A.SetNo, A.ScrollNo, A.SubGlCode, A.AccNo, A.PartiCulars, " +
                  "(Case When A.TrxType = '1' Then A.Amount Else '0' End) As CREDIT, " +
                  "(Case When A.TrxType = '2' Then A.Amount Else '0' End) As DEBIT, U.LoginCode As Maker From " + TBNAME + " A With(NoLock) " +
                  "Left Join UserMaster U With(NoLock) On A.MID = U.PERMISSIONNO " +
                  "WHERE A.BRCD = '" + BrCode + "' And A.EntryDate = '" + conn.ConvertDate(EDate) + "' " +
                  "And A.SetNo Between '" + FSetNo + "' And '" + TSetNo + "' And A.Stage Not In (1003, 1004) " +
                  "And A.SetNo < 20000 And Activity Not In (31, 32) And PmtMode Not In ('IC','OC') " +
                  "Order By EntryDate, SetNo, ScrollNo";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetUntallySet(GridView GView, string BrCode, string EDate, string FSetNo, string TSetNo, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0184 @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @FSetNo = '" + FSetNo + "', @TSetNo = '" + TSetNo + "', @Mid = '" + Mid + "'";
            Result = conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetSMS_Data(string BrCode, string EDate)
    {
        try
        {
            sql = "Select CustNo, Mobile, ConVert(VarChar(10), SMS_Date, 103) As SMS_Date, SMS_Type, SMS_Description From AVS1092 " +
                  "Where BrCd = '" + BrCode + "' And SMS_Date = '" + conn.ConvertDate(EDate) + "' And PcMac = 'LotPass'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}