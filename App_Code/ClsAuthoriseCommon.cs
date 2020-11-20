using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAuthoriseCommon
{
    ClsEncryptValue CanRes = new ClsEncryptValue();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", VerifyCode = "", sResult = "", TableName = "";
    double Balance = 0;
    int Result = 0;

	public ClsAuthoriseCommon()
	{
		
	}

    //  Added by Amol ON 17/11/2017 For authorization (Common Function)
    #region Common Function

    public string GetProdName(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select GLNAME From GLMAST Where BRCD = '" + BrCode + "' And SUBGLCODE = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetCustName(string BrCode, string PrCode, string AccNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select M.CustName +'_'+ ConVert(VarChar(10), A.CustNo) As CustName From Master M Inner Join Avs_Acc A ON A.CUSTNO = M.CUSTNO "+
                  "Where A.BRCD = '" + BrCode + "' And A.SUBGLCODE = '" + PrCode + "' And A.ACCNO = '" + AccNo + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
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

    public string GetFinStartDate(string EDate)
    {
        try
        {
            sql = "Exec SP_GetCurrFinStartDate '" + conn.ConvertDate(EDate).ToString() + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            Balance = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Balance;
    }

    public string GetAccType(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select Description From LookUpForm1 Where Lno = '1017' And SrNo = (Select Opr_Type From Avs_Acc " +
                  "Where BrCd = '" + BrCode + "' And SubGlcode = '" + PrCode + "' And AccNo = '" + AccNo + "')";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetJointName(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select Replace(JointName, '-' ,' ') As JointName From Joint Where BrCd = '" + BrCode + "' And SubGlcode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetPanCardNo(string BrCode, string CustNo)
    {
        try
        {
            sql = "Select Doc_No From Identity_Proof where Doc_Type = '3' And BrCd = '" + BrCode + "' And CustNo = '" + CustNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetCustNo(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select CustNo From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlcode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public void BindReason(DropDownList ddlList)
    {
        try
        {
            sql = "Select Description As Name, SrNo As Id From LookUpForm1 Where LNo = 2501 And LType = 'VCancel'";
            conn.FillDDL(ddlList, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string GetVoucherCount(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select Count(*) From " + TableName + " Where BrCd = '" + BrCode + "' And SubGlCode = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string CheckVoucherStage(string BrCode, string EDate, string SetNo, string ScrollNo)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            if (ScrollNo != "")
            {
                sql = "Select Stage From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "'";
                sResult = conn.sExecuteScalar(sql);
            }
            else
            {
                sql = "Select Stage From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                sResult = conn.sExecuteScalar(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetAccStatDetails(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string BRCD)
    {
        try
        {
            DT = new DataTable();

            sql = "Exec SP_ACCSTATUS_R @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "', @BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetPayMast(string BrCode, string EDate, string SetNo, string ScrollNo)
    {
        try
        {
            sql = "Exec SP_VoucherAutho @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @SetNo = '" + SetNo + "', @ScrollNo = '" + ScrollNo + "', @Flag = 'GET_PAYMAST', @sFlag = ''";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    #endregion
    //  End Added by Amol ON 17/11/2017

    #region Authorize Functions

    public DataTable GetDetails_ToFill(string BrCode, string EDate, string SetNo, string PayMast, string ScrollNo)
    {
        try
        {
            sql = "Exec SP_VoucherAutho @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @SetNo = '" + SetNo + "', @ScrollNo = '" + ScrollNo + "', @Flag = '" + PayMast + "', @sFlag = ''";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

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

    public int VoucherAuthorizeCRCP(string BrCode, string EDate, string SetNo, string Mid, string ScrollNo)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            VerifyCode = CanRes.GetCK(Mid.ToString());
            sql = "Update " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + VerifyCode + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                conn.sExecuteQuery(sql);

                sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "'";
                conn.sExecuteQuery(sql);

                sql = "Select Count(*) From " + TableName + " WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And Stage Not In ('1003', '1004')";
                sResult = conn.sExecuteScalar(sql);

                if (sResult == "1")
                {
                    sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + VerifyCode + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And SubGlCode = '99'";
                    Result = conn.sExecuteQuery(sql);

                    sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And SubGlCode = '99'";
                    conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int VoucherCancelCRCP(string BrCode, string EDate, string SetNo, string Reason, string Mid, string ScrollNo)
    {
        try
        {
            VerifyCode = CanRes.GetVK(Mid.ToString());
            sql = "EXEC SP_CANCEL_ENTRY @FLAG='CRCP',@SETNO='" + SetNo + "',@EDT='" + conn.ConvertDate(EDate) + "',@BRCD='" + BrCode + "',@MID='" + Mid + "', @F3 = '" + VerifyCode.ToString() + "', @Reason = '" + Reason.ToString() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int VoucherAuthorizeLoan(string BrCode, string EDate, string SetNo, string Mid, string ScrollNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            string TBNAME = "";
            TBNAME = TD[2].ToString() + TD[1].ToString();

            VerifyCode = CanRes.GetCK(Mid.ToString());
            sql = "UPDATE AVSM_" + TBNAME + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + VerifyCode + "' Where BrCd = '" + BrCode + "' AND EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                Result = conn.sExecuteQuery(sql);
                sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' Where BrCd = '" + BrCode + "' AND EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int VoucherCancelLoan(string BrCode, string PrCode, string AccNo, string EDate, string SetNo, string Reason, string Mid, string ScrollNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Top 1 RefId From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' Order By ScrollNo";
            sResult = conn.sExecuteScalar(sql);

            if (sResult != null && sResult != "")
            {
                VerifyCode = CanRes.GetVK(Mid.ToString());
                sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + Mid + "', F3 = '" + VerifyCode + "', CReason = '" + Reason + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1004 Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "'";
                    Result = conn.sExecuteQuery(sql);

                    // Added by amol On 31/01/2018 for increase and decrease cash
				    sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) "+
                          "From AVS5012 A " +
				          "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type "+
                          "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + conn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                    conn.sExecuteQuery(sql);

                    // Added by amol On 30/01/2018 for cancel cash denomination voucher
                    sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "UPDATE AllVcr SET STAGE = 1004, CID = '" + Mid + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "'";
                    conn.sExecuteQuery(sql);

                    if (Result > 0)
                    {
                        sql = "Update LoanInfo Set LASTINTDATE = PREV_INTDT, LMSTATUS = 1, MOD_DATE = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd = '" + BrCode + "' And LOANGLCODE = '" + PrCode + "' And CUSTACCNO = '" + AccNo + "'";
                        Result = conn.sExecuteQuery(sql);
                    }

                    //  for account is suit file
                    if (Result > 0)
                    {
                        sql = "Select IsNull(Case When Acc_Status = '' Then '1' Else Acc_Status End, '1') From AVS5032 Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
                        sResult = conn.sExecuteScalar(sql);

                        if (sResult == "" || sResult == null)
                            sResult = "1";

                        sql = "Update Avs_Acc Set ACC_STATUS = '" + sResult + "' Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    #endregion

    #region Move Data from M_Table

    public int MoveVoucher(string BrCode, string EDate, string SetNo, string Mid)
    {
        try
        {
            sql = "Exec SP_MoveVoucherTans @BrCode = '" + BrCode + "', @WorkDate = '" + conn.ConvertDate(EDate).ToString() + "', @SetNo = '" + SetNo + "', @UserMid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    #endregion

}