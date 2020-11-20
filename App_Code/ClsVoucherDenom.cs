using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public class ClsVoucherDenom
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "", TableName = "";
    int Result = 0;

	public ClsVoucherDenom()
	{
		
	}

    public string CheckCashSet(string BrCode, string EntryDate, string SetNo)
    {
        try
        {
            string[] TD = EntryDate.ToString().Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Top 1 Activity From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' "+
                  "And SetNo = '" + SetNo + "' And Activity In ('3', '4') And Stage <> '1004' And PmtMode Not in ('TR_INT', 'TR-INT')";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string CheckDenom(string BrCode, string SetNo, string EntryDate)
    {
        try
        {
            sql = "Select SetNo From Avs5012 Where BrCd = '" + BrCode + "' And EffectDate = '" + conn.ConvertDate(EntryDate).ToString() + "' "+
                  "And SetNo = '" + SetNo + "' And Stage <> '1004'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int GetSpecificVoucher(GridView grid, string BrCode, string EntryDate, string SetNo)
    {
        try
        {
            string[] TD = EntryDate.ToString().Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select T.BrCd, T.SetNo, ConVert(VarChar(103), T.EntryDate, 103) As EntryDate, T.SubGlcode, T.AccNo, T.Amount, (Case When T.TrxType = 1 Then 'Cr' Else 'Dr' End) As CrDr, " +
                  "A.LoginCode As Maker, B.LoginCode As Checker, C.LoginCode As Deleted From " + TableName + " T " +
                  "Left Join UserMaster A On A.PermissionNo = T.Mid " +
                  "Left Join UserMaster B On B.PermissionNo = T.Vid " +
                  "Left Join UserMaster C On C.PermissionNo = T.Cid " +
                  "Where T.BrCd = '" + BrCode + "' And T.EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' And T.SetNo = '" + SetNo + "' "+
                  "And T.Stage <> '1004' And Activity In ('3', '4') And T.PmtMode Not in ('TR_INT', 'TR-INT') Order By SetNo, ScrollNo ";
            Result = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetAllVoucher(GridView grid, string BrCode, string EntryDate)
    {
        try
        {
            string[] TD = EntryDate.ToString().Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select T.BrCd, T.SetNo, ConVert(VarChar(103), T.EntryDate, 103) As EntryDate, T.SubGlcode, T.AccNo, T.Amount, (Case When T.TrxType = 1 Then 'Cr' Else 'Dr' End) As CrDr, " +
                  "A.LoginCode As Maker, B.LoginCode As Checker, C.LoginCode As Deleted From " + TableName + " T " +
                  "Left Join UserMaster A On A.PermissionNo = T.Mid "+
                  "Left Join UserMaster B On B.PermissionNo = T.Vid "+
                  "Left Join UserMaster C On C.PermissionNo = T.Cid "+
                  "Where T.BrCd = '" + BrCode + "' And T.EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' " +
                  "And T.SetNo Not In (Select SetNo From Avs5012 D Where D.BrCd = T.BrCd And D.EffectDate = T.EntryDate And D.SetNo = T.SetNo And D.Stage <> '1004') "+
                  "And T.Stage <> '1004' And Activity In ('3', '4') And T.PmtMode Not in ('TR_INT', 'TR-INT') Order By SetNo, ScrollNo ";
            Result = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}