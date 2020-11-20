using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAuthoVoucher
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string EntryMid, verifyMid, DeleteMid = "";
    string sResult = "", sql = "", TableName = "";
    int Result = 0;

    public ClsAuthoVoucher()
    {

    }

    public string GetAuthoParam(string BrCode)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '0' And ListField = 'AUTALL'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string Getaccno(string AT, string BRCD)
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

    public DataTable GetSetMid(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Distinct Mid, Stage From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetSetInfo(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select * From " + TableName + " Where GlCode in (1,3,5,8) And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "'";

          //  sql = "Select * From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }





    public DataTable GetAccountWisePayRollData(string Month,string Year, string EDate, string SetNo)
    {
        try
        {

            string tableName = "AVSM_" + Year + Month;
            sql = " exec SpGetLoanData @entrydate='" + EDate + "',@setno='" + SetNo + "'";
          //  sql = "select  ACCNO,SUM(Amount) CREDIT,BRCD from " + tableName + " where entrydate='" + EDate + "' and SetNo='" + SetNo + "' and TrxType='1' and GlCode in (3,11) group by AccNo,BRCD";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetInfoTable(GridView Gview, string STR, string BRCD, string EDT)
    {
        try
        {
            string[] TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select SetNo, (Convert(VarChar(10), SetNo)+'_'+Convert(VarChar(10), ScrollNo)+'_'+Convert(VarChar(10),MID)) ScrollNo, ScrollNo ScrollNo1, SubGlCode, AccNo, Particulars, " +
                "(Case When TrxType = '1' Then Amount Else '0' End) As Credit, " +
                "(Case When TrxType = '2' Then Amount Else '0' End) As Debit, " +
                "(Case When A.Stage = '1001' Then 'NotAuthorise' When A.Stage = '1003' Then 'Authorised' When A.Stage = '1004' Then 'Canceled' End) As Status, " +
                "U.LoginCode From " + TableName + " A " +
                "Left Join USERMASTER U On A.MID=U.Permissionno " +
                "Where A.RefBrcd = '" + BRCD + "' And A.Stage Not In('1003', '1004') " + STR + " And SetNo > 20000 And SubGlCode <> 99 and A.SubGlCode <> 0 And Activity Not In(31,32) " +
                "Order By EntryDate, SetNo, ScrollNo";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetInfoTableAdmin(GridView Gview, string STR, string BRCD, string EDT)
    {
        try
        {
            string[] TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select SetNo, (Convert(VarChar(10), SetNo)+'_'+Convert(VarChar(10), ScrollNo)+'_'+Convert(VarChar(10),MID)) ScrollNo, ScrollNo ScrollNo1, SubGlCode, AccNo, Particulars, " +
                "(Case When TrxType = '1' Then Amount Else '0' End) As Credit, " +
                "(Case When TrxType = '2' Then Amount Else '0' End) As Debit, " +
                "(Case When A.Stage = '1001' Then 'NotAuthorise' When A.Stage = '1003' Then 'Authorised' When A.Stage = '1004' Then 'Canceled' End) As Status, " +
                "U.LoginCode From " + TableName + " A " +
                "Left Join USERMASTER U On A.MID=U.Permissionno " +
                "Where A.RefBrcd = '" + BRCD + "' And A.Stage Not In('1004') " + STR + " And SetNo > 20000 And SubGlCode <> 99 and A.SubGlCode <> 0 And Activity Not In(31,32) " +
                "Order By EntryDate, SetNo, ScrollNo";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthoriseEntryLoan(string BrCode, string RefBrcd, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            verifyMid = Ecry.GetCK(Mid.ToString());

            sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE SETNO = '" + SetNo + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + EntryMid + "' WHERE SETNO = '" + SetNo + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    Result = conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            Result = 0;
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result==null?0:Result;
    }

    public int CancelEntryLoan(string BrCode, string RefBrcd, string PrCode, string AccNo, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(Mid.ToString());

            sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + Mid + "', F3 = '" + DeleteMid + "' WHERE SETNO = '" + SetNo + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                Result = conn.sExecuteQuery(sql);

                // Added by amol On 31/01/2018 for increase and decrease cash
                sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                      "From AVS5012 A " +
                      "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                      "Where A.EffectDate = '" + conn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                conn.sExecuteQuery(sql);

                // Added by amol On 30/01/2018 for cancel cash denomination voucher
                sql = "Update avs5012 Set Stage = '1004' Where EffectDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update LoanInfo Set LASTINTDATE = PREV_INTDT, MOD_DATE = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + RefBrcd + "' And LOANGLCODE = '" + PrCode + "' And CUSTACCNO = '" + AccNo + "'";
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

    public int AuthoriseEntry(string BrCode, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            verifyMid = Ecry.GetCK(Mid.ToString());

            if (Convert.ToInt32(SetNo) > 20000)
            {
                sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "'";
                    Result = conn.sExecuteQuery(sql);
                }
                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
            else
            {
                sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "'";
                    Result = conn.sExecuteQuery(sql);
                }
                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BRCD = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    Result = conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            Result = 0;
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result==null?0:Result;
    }

    public int CancelEntry(string BrCode, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(Mid.ToString());

            if (Convert.ToInt32(SetNo) > 20000)
            {
                sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + Mid + "', F3 = '" + DeleteMid + "' WHERE ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    // Added by amol On 31/01/2018 for increase and decrease cash
                    sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                          "From AVS5012 A " +
                          "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                          "Where A.EffectDate = '" + conn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                    conn.sExecuteQuery(sql);

                    // Added by amol On 30/01/2018 for cancel cash denomination voucher
                    sql = "Update avs5012 Set Stage = '1004' Where EffectDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "UPDATE ALLVCR SET STAGE = 1004, CID = '" + Mid + "' WHERE ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "' ";
                    Result = conn.sExecuteQuery(sql);
                }
            }
            else
            {
                sql = "UPDATE ALLVCR SET STAGE = 1004, CID = '" + Mid + "' WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + Mid + "', F3 = '" + DeleteMid + "' WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' AND SETNO = '" + SetNo + "'";
                    Result = conn.sExecuteQuery(sql);
                }
                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BRCD = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result==null?0:Result;
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

    public string GetGlCode(string BrCode, string IntR)
    {
        try
        {
            sql = " Select SubGlCode From GlMast Where BrCd = '" + BrCode + "' And IR = '" + IntR + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public string GetAccStatus(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = " Select Acc_Status From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlCode = '" + ProdCode + "' And AccNo = '" + AccNo + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public int GetVoucherInfo(GridView Gview, string STR, string BRCD, string EDT)
    {
        try
        {
            string[] TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT Setno,(CONVERT(varchar(10), setno)+'_'+CONVERT(varchar(10),Scrollno)+'_'+CONVERT(varchar(10),MID)) Scrollno, Scrollno Scrollno1,SUBGLCODE,ACCNO,Particulars,(case when trxtype='1' then AMOUNT else '0' end) CREDIT, " +
                " (case when trxtype='2' then AMOUNT else '0' end) DEBIT,U.LOGINCODE From " + TableName + " A " +
                " LEFT JOIN USERMASTER U ON A.MID=U.Permissionno " +
                " WHERE A.RefBrcd='" + BRCD + "' AND A.STAGE=1001 " + STR + " AND SetNo > 20000 AND SUBGLCODE<>99 and A.SUBGLCODE<>0 and ACTIVITY NOT IN (31,32) order by Entrydate,setno,scrollno";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable VGetInfo(string BRCD, string ST, string SRNO, string EDT)
    {
        try
        {
            string[] TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT M.CUSTNO, A.Setno,A.Scrollno,A.SUBGLCODE,A.ACCNO,A.Particulars,AMOUNT AMT ,A.ENTRYDATE,A.PCMAC,U.LOGINCODE MID,M.CUSTNAME,GL.GLNAME,A.SUBGLCODE FROM " + TableName + " A " +
                  " LEFT JOIN AVS_ACC AC ON AC.ACCNO=A.ACCNO AND AC.SUBGLCODE=A.SUBGLCODE AND AC.BRCD=A.BRCD " +
                  " LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO " +
                  " LEFT JOIN USERMASTER U ON A.MID=U.Permissionno " +
                  " LEFT JOIN GLMAST GL ON A.GLCODE=GL.GLCODE AND A.SUBGLCODE=GL.SUBGLCODE AND A.BRCD=GL.BRCD WHERE A.BRCD='" + BRCD + "' AND A.SETNO='" + ST + "' AND A.SCROLLNO='" + SRNO + "' AND A.ENTRYDATE='" + conn.ConvertDate(EDT) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    //Added By Amol on 07/11/2017 as per darade sir instruction
    public string CheckVoucher(string BrCode, string EDate, string VoucherNo)
    {
        try
        {
            sql = "Exec SP_CheckVoucher @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @SetNo = '" + VoucherNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetPAYMAST(string SETNO, string BRCD, string EDT)
    {
        try
        {
            sql = "EXEC SP_AUTHOVOUCHER @FLAG='GET_PAYMAST',@SETNO='" + SETNO + "',@EDT='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public DataTable GetDetails_ToFill(string SETNO, string BRCD, string EDT, string FL)
    {
        try
        {
            sql = "EXEC SP_AUTHOVOUCHER @FLAG='" + FL + "', @SETNO='" + SETNO + "', @EDT='" + conn.ConvertDate(EDT).ToString() + "', @BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int UpdateCommissionstage(string entrydate,string setno)
    {
        sql = "UPDATE AVS_AGENTCOMIOSSION SET STAGE=1004 WHERE entrydate='" + conn.ConvertDate(entrydate).ToString() + "' and setno='" + setno + "' ";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
}