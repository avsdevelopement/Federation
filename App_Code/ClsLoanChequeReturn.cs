using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanChequeReturn
{
    ClsEncryptValue Ecry = new ClsEncryptValue(); 
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string EntryMid, verifyMid, DeleteMid = "";
    string sql = "", sResult = "", PA = "";
    string Year = "", Month = "";
    string TableName = "";
    int Result = 0;

    public ClsLoanChequeReturn()
    {
    }

    public string GetLoanAccNo(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string Getstage1(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select Convert(varChar(5), STAGE) From Avs_Acc Where BRCD='" + BrCode + "' And SUBGLCODE='" + ProdCode + "' And ACCNO='" + AccNo + "' And STAGE <>1004 ";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public string GetEntryMid(string BrCode, string PrCode, string AccNo, string ChequeNo, string ChequeDate, string EDate)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select Mid From " + TableName + " Where Brcd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' " +
                  "And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And InstrumentNo = '" + ChequeNo + "' And InstrumentDate = '" + conn.ConvertDate(ChequeDate).ToString() + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetTransaction(string BrCode, string ProdCode, string AccNo, string ChequeNo, string ChequeDate, string EntryDate)
    {
        try
        {
            string[] Date = EntryDate.Split('/');
            TableName = "AVSM_" + Date[2].ToString() + "" + Date[1].ToString();

            //Removed by ankita 18/08/2017 by suggestion of darade sir
            //sql = "Select A.GlCode, A.SubGlCode, A.AccNo, A.SetNo, Convert(VarChar(10), A.EntryDate, 103) As EntryDate, A.Particulars2, A.Amount, " +
            //      " (Case When A.TrxType = 2 Then 'Dr' Else 'Cr' End) As DbCrIndicator, A.InstrumentNo, A.Mid,u.USERNAME From Avsm_" + Year + "" + Month + " A inner join USERMASTER U on A.brcd=U.BRCD and a.mid=u.PERMISSIONNO" +
            //      " Where A.Brcd = '" + BrCode + "' and A.InstRuMentNo = '" + ChequeNo + "' And A.InstruMentDate = '" + conn.ConvertDate(ChequeDate).ToString() + "' And A.EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' And A.PmtMode <> 'TR_INT'";

            //added by ankita 18/08/2017 by suggestion of darade sir
            //sql = "Select A.GlCode, A.SubGlCode, A.AccNo, A.SetNo, Convert(VarChar(10), A.EntryDate, 103) As EntryDate, A.Particulars2, A.Amount, (Case When A.TrxType = 2 Then 'Dr' Else 'Cr' End) As DbCrIndicator, " +
            //      " A.InstrumentNo, Convert(VarChar(10), A.InstrumentDate, 103) As InstrumentDate, A.Mid,u.USERNAME From Avsm_" + Year + "" + Month + " A Left Join USERMASTER U on a.mid=u.PERMISSIONNO" +
            //      " Where A.Brcd = '" + BrCode + "' And A.Subglcode='" + ProdCode + "' And A.Accno='" + AccNo + "' And A.EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' And A.PmtMode <> 'TR_INT'";

            //Added By Amol On 18/12/2017 As per darade sir instruction
            sql = "Select A.GlCode, A.SubGlCode, A.AccNo, A.SetNo, Convert(VarChar(10), A.EntryDate, 103) As EntryDate, A.Particulars2, A.Amount, (Case When A.TrxType = 2 Then 'Dr' Else 'Cr' End) As DbCrIndicator, " +
               " A.InstrumentNo, Convert(VarChar(10), A.InstrumentDate, 103) As InstrumentDate, A.Mid,u.USERNAME From " + TableName + " A " +
               " Left Join UserMaster U On A.Mid = U.PermissionNo " +
               " Where A.Brcd = '" + BrCode + "' And A.EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' And A.PmtMode <> 'TR_INT' And A.TrxType = '1' " +
               " And A.SetNo In (Select SetNo From Avs_LnTrx B Where B.Brcd = '" + BrCode + "' And B.LoanGlCode = '" + ProdCode + "' And B.AccountNo = '" + AccNo + "' " +
               " And B.EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' And B.Stage = '1003') And A.Stage = '1003' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string CreateNewSet(string BrCode, string PrCode, string AccNo, string ChequeNo, string ChequeDate, string EntryDate, string WorkDate, string Mid)
    {
        try
        {
            sql = "Exec RptReturnEntryCreate @BrCode = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @ChequeNo = '" + ChequeNo + "', @ChequeDate = '" + conn.ConvertDate(ChequeDate).ToString() + "', @EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "', @WorkDate = '" + conn.ConvertDate(WorkDate).ToString() + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME().ToString() + "', @Flag = 'AD'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int AuthoriseNewSet(string BrCode, string PrCode, string AccNo, string ChequeNo, string ChequeDate, string EntryDate, string WorkDate, string Mid)
    {
        try
        {
            sql = "Exec RptReturnEntryCreate @BrCode = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @ChequeNo = '" + ChequeNo + "', @ChequeDate = '" + conn.ConvertDate(ChequeDate).ToString() + "', @EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "', @WorkDate = '" + conn.ConvertDate(WorkDate).ToString() + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME().ToString() + "', @Flag = 'AT'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteNewSet(string BrCode, string PrCode, string AccNo, string ChequeNo, string ChequeDate, string EntryDate, string WorkDate, string Mid)
    {
        try
        {
            sql = "Exec RptReturnEntryCreate @BrCode = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @ChequeNo = '" + ChequeNo + "', @ChequeDate = '" + conn.ConvertDate(ChequeDate).ToString() + "', @EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "', @WorkDate = '" + conn.ConvertDate(WorkDate).ToString() + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME().ToString() + "', @Flag = 'DL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}