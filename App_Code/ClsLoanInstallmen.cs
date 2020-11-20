using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanInstallmen
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sResult, sql = "", PA = "", instDate = "";
    int Result = 0;
    bool res = true;

	public ClsLoanInstallmen()
	{
		
	}

    public string openDay(string BRCD)
    {
        string wdt = "";
        try
        {
            sql = "Select ListValue From Parameter Where ListField = 'DayOpen' And BrCd = '" + BRCD + "' ";
            wdt = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return wdt;
    }

    public string GetLabelName(string SrNumber)
    {
        try
        {
            sql = "Select HeadDesc From AVS_LnTrx_Head Where SrNumber = '" + SrNumber + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DT = new DataTable();
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

    public string GetCustNo(string brcd, string SubGlCode, string AccNo)
    {
        string Accname = "";
        try
        {
            string sql = "SELECT B.CUSTNO FROM AVS_ACC A, MASTER B WHERE A.CUSTNO = B.CUSTNO AND A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "'";
            Accname = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Accname;
    }

    public string CheckLock()
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = 0 And ListField = 'LoanAmountLock'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetIntACCYN(string BRCD, string SBGL)
    {
        try
        {
            sql = "SELECT ISNULL(INTACCYN,'N') FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "'";
            SBGL = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return SBGL;
    }

    public DataTable GetAllFieldData(string SubGlCode, string AccNo, string BrCode, string EDate)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec RptLoanDetailsInfo @BrCode = '" + BrCode + "', @SubGlCode = '" + SubGlCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'LDI'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAllBalData(string BrCode, string SubGlCode, string AccNo, string EDate)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec SP_LoanInstBalances @BrCode = '" + BrCode + "', @SGlCode = '" + SubGlCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetADMSubGl(string BrCode)
    {
        DT = new DataTable();
        try
        {
            sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAllTrx(string BrCode, string SubGlCode, string AccNo, string EDate)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec SP_GetAllTransaction @BrCode = '" + BrCode + "', @SGlCode = '" + SubGlCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public bool ChkSheduleExists(string BrCode, string CustNo, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Top 1 1 From LoanSchedule Ls With(NoLock)" +
                  " Where Ls.Brcd = '" + BrCode + "' And Ls.LoanglCode = '" + SubGlCode + "' And Ls.CustAccNo = '" + AccNo + "' And Ls.Stage = 1003";
            PA = conn.sExecuteScalar(sql);

            if (PA == null|| PA == "")
                res = false;
            else
                res = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }

    public string GetDrawPower(string BrCode, string CustNo, string SubGlCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Select Balance From LoanSchedule Ls With(NoLock)" +
                  " Where Ls.Brcd = '" + BrCode + "' And Ls.LoanglCode = '" + SubGlCode + "' And Ls.CustAccNo = '" + AccNo + "' And Ls.Stage = 1003" +
                  " And Ls.INSTDATE = (Select Max(INSTDATE) From LOANSCHEDULE L" +
                  "             Where Ls.BrCd = L.BrCd And Ls.CustNo = L.CustNo And Ls.LoanglCode = L.LoanglCode" +
                  "             And Ls.CustAccNo = L.CustAccNo And L.INSTDATE <= '" + conn.ConvertDate(EDate).ToString() + "' And L.Stage = 1003)";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public double GetOtherIntRate(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select OTHERCHG From LOANGL Where BrCd = '" + BrCode + "' and LoanGlCode='" + SubGlCode + "'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA == "" ? "0.00" : PA);
    }

    public double PrAmt(string BRCD, string sGlCode, string AccNo, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + sGlCode + "','" + AccNo + "','" + conn.ConvertDate(Edate).ToString() + "','PrAmt'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public double IntAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','IntAmt'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public double PenalIntAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','PIntAmt'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public double BalAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','Bal'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA == "" ? "0.00" : PA);
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public int CreateTable(string TABLENAME)
    {
        try
        {
            sql = "CREATE TABLE " + TABLENAME + "(ID INT IDENTITY(1,1), BRCD Varchar(05), SUBGLCODE VarChar(10), ACCNO VarChar(16), PARTICULARS NVarChar(100), AMOUNT Numeric(18, 2), TRXTYPE Int, ACTIVITY Int, PMTMODE VarChar(15), STAGE VarChar(4), LOGINCODE VarChar(10), MID VarChar(5), CID VarChar(5), VID VarChar(5), ENTRYDATE DATETIME)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GETDATA(string BRCD, string GLCODE, string SGLCODE)
    {
        DT = new DataTable();
        try
        {
            sql = "SELECT GLCODE, SUBGLCODE, PLACCNO, IR, IOR FROM GLMAST WITH(NOLOCK) WHERE BRCD = '" + BRCD + "' AND GLCODE = '" + GLCODE + "' AND SUBGLCODE = '" + SGLCODE + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetIntCal(string BrCode, string SGlCode)
    {
        try
        {
            sql = "Select IntCalType From LoanGl Where Brcd = '" + BrCode + "' And LoanGlCode='" + SGlCode + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetIntApp(string BrCode, string SGlCode)
    {
        try
        {
            sql = "Select Int_App From LoanGl Where Brcd = '" + BrCode + "' And LoanGlCode='" + SGlCode + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string CheckStaff(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select LoanCategory From LoanGl Where BrCd = '" + BrCode + "' And LoanGlCode = '" + PrCode + "'";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public int UpdateLastIntDate(string BrCode, string SGlCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + BrCode + "' And LOANGLCODE = '" + SGlCode + "' And CUSTACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public double GetLoanInterest(string BrCode, string GlCode, string SubGlCode, string AccNo, string LastIntDate, string Edate)
    {
        try
        {
            sql = "Exec RptLoanInterest '" + BrCode + "','" + GlCode + "','" + SubGlCode + "','" + AccNo + "','" + conn.ConvertDate(LastIntDate).ToString() + "','" + conn.ConvertDate(Edate).ToString() + "'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public int CloseLoanAcc(string BRCD, string SubglCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set LmStatus = 99, Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Stage = 1003, Vid = '" + Mid + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BRCD = '" + BRCD + "' AND LOANGLCODE = '" + SubglCode + "' AND CUSTACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);

            if (Convert.ToInt32(Result) > 0)
            {
                sql = "Update Avs_Acc Set Acc_status = 3, Stage = 1003, LastIntDt = '" + conn.ConvertDate(EDate).ToString() + "', CLOSINGDATE = '" + conn.ConvertDate(EDate).ToString() + "', Vid = '" + Mid + "' Where BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SubglCode + "' AND ACCNO = '" + AccNo + "'";
                Result = conn.sExecuteQuery(sql);
            }

            //  Added by amol on 10-10-2017 because of remove lien against loan
            if (Result > 0)
            {
                sql = "Select BRCD, DepositGlCode, DepositAccNo From Avs_LienMarkDetails Where BrCd = '" + BRCD + "' And LoanGlCode = '" + SubglCode + "' And LoanAccno = '" + AccNo + "'";
                DT = new DataTable();

                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        sql = "Update Avs_Acc Set Acc_Status = '1' Where BrCd = '" + DT.Rows[i]["BRCD"].ToString() + "' And SubGlCode = '" + DT.Rows[i]["DepositGlCode"].ToString() + "' And AccNo = '" + DT.Rows[i]["DepositAccNo"].ToString() + "'";
                        conn.sExecuteQuery(sql);
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

    public string GetAccOpenDate(string BrCode, string CustNo, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Convert(VarChar(10), OpeningDate, 103) From Avs_Acc Where Brcd = '" + BrCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "'";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public string GetMigDate(string BrCode)
    {
        try
        {
            sql = "Select Convert(VarChar(10), Implementatin, 120) From BankName Where Brcd = '" + BrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetLoanSchedule(string FL, string SBGL ,string ACCNO, string BRCD)
    {
        DT = new DataTable();
        try
        {
            sql = "EXEC SP_REPORT_LOANSCHEDULE @FLAG='" + FL + "',@SBGL='" + SBGL + "',@ACCNO='" + ACCNO + "',@BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable FetchNomineeDetails(string CustNO, string ACCno, string BRCD)
    {
        DT = new DataTable();
        try
        {
            sql = "select  ISNULL(S_NAME,'')+' '+ISNULL(SF_NAME,'')+' '+ISNULL(SL_NAME,'') as Nominee from SURITY where loanACCNo='118' AND LoanGlcode='" + ACCno + "' and BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int InsertIntoTable(string BrCode, string CustNo, string GlCode, string SubGlCode, string AccNo, string CustName, string Amount, string TrxType, string Activity, string PmtMode, string Perticulars, string Perticulars2, string ChkNo, string ChkDate, string EDate, string Mid)
    {
        try
        {
            sql = "Insert Into Avs_TempABBMultiTrf (BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, RefId, InstNo, InstDate, EntryDate, SystemDate, Mid) " +
                  "VALUES ('" + BrCode + "', '" + CustNo + "','" + CustName + "', '" + GlCode + "','" + SubGlCode + "','" + AccNo + "','" + Perticulars + "','" + Perticulars2 + "','" + Amount + "','" + TrxType + "','" + Activity + "','" + PmtMode + "','0','" + ChkNo + "','" + conn.ConvertDate(ChkDate).ToString() + "', '" + conn.ConvertDate(EDate).ToString() + "', GetDate(), '" + Mid + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetCRDR(string BrCode, string EDate, string Mid)
    {
        DT = new DataTable();
        try
        {
            sql = "Select Abs(IsNull(Sum(A.Debit), 0)) As DEBIT From( " +
                  "Select (Case When TRXTYPE = '2' Then Sum(Amount) Else '0' End) As Debit From Avs_TempABBMultiTrf " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' " +
                  "Group By TrxType)A ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int Getinfotable(GridView Gview, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select ID, SubGlCode, AccNo, CustName, Amount, Particulars, (Case When TRXTYPE = 1 Then 'Cr' Else 'Dr' End) As TrxType From Avs_TempABBMultiTrf Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' Order By SystemDate Desc";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteSingleRecTable(string id, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From Avs_TempABBMultiTrf Where ID = '" + id + "' And BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void DelAllRecTable(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From Avs_TempABBMultiTrf Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetDRTransDetails(string BrCode, string EDate, string Mid)
    {
        DT = new DataTable();
        try
        {
            sql = "Select BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Sum(Amount) As Amount, "+
                "TrxType, Activity, PmtMode, RefId, InstNo, Convert(VarChar(10), InstDate, 103) As InstDate, EntryDate, Mid "+
                "From Avs_TempABBMultiTrf Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And TrxType = '2' "+
                "Group By BrCd, GlCode, SubGlCode, AccNo, CustNo, CustName, Particulars, Particulars2, TrxType, Activity, PmtMode, RefId, InstNo, InstDate, EntryDate, Mid";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable FetchLoanDetails(string CustNO, string ACCno, string BRCD,string GLCode)
    {
        DT = new DataTable();
        try
        {
            sql = "select * from loaninfo where CustACCno='" + ACCno + "' and BRCD='" + BRCD + "' and Custno='" + CustNO + "' and loanglcode='"+GLCode+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable FetchOpeningDate(string CustNO, string ACCno, string BRCD, string GLCode)
    {
        DT = new DataTable();
        try
        {
            sql = "SELECT MOBILE, CONVERT(NVARCHAR(10),OPENINGDATE,103) AS OD FROM MASTER WHERE CUSTNO='" + CustNO + "' AND CUSTACCNO='" + ACCno + "' AND GLCODE='" + GLCode + "' AND BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int GetBorrower(GridView grd,string brcd, string accno, string subgl)
    {
        
        try
        {
            sql = "select LnSrName from AVSLnSurityTable where LnType='Loanee' and brcd='" + brcd + "' and AccNo='" + accno + "' and SubGlCode='" + subgl + "'";
            Result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public string GetAccStatus(string BrCode, string PrCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Select IsNull(AccStatus, 0) As AccStatus From (Select Max(IsNull(A.Acc_Status, 9)) As AccStatus From AVS5032 A " +
                  "Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + PrCode + "' And A.AccNo = '" + AccNo + "' " +
                  "And A.EffectDate = (Select Max(B.EffectDate) From AVS5032 B Where A.BrCd = B.BrCd And A.SubGlCode = B.SubGlCode And A.AccNo = B.AccNo))A";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

   

    public double GetSurIntRate(string AccStatus)
    {
        try
        {
            sql = "Select Cast(DescriptionMar As Float) As IntRate From LookUpForm1 Where Lno = '1047' And SrNo = '" + AccStatus + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sResult);
    }

    public string GetAccType(string AccStatus)
    {
        try
        {
            sql = "Select Description From LookUpForm1 Where Lno = '1047' And SrNo = '" + AccStatus + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetAvsAccStatus(string BrCode, string PrCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Select SubAccStatus From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlcode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetGSTFlag()
    {
        DT = new DataTable(); 
        try
        {
            sql = "Select SrNumber, GSTFlag From AVS_LnTrx_Head Order By SrNumber";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GstDetails(string BrCode)
    {
        DT = new DataTable();
        try
        {
            sql = "Select A.GST, G.GlCode As GSTGlCode, A.PrdCd, G.GlName, A.SGST, G2.GlCode As SGSTGlCode, A.SGSTPrdCd, G2.GlName As SGSTGlName, " +
                  "A.CGST, G1.GlCode As CGSTGlCode, A.CGSTPrdCd, G1.GlName As CGSTGlName From GstMaster A " +
                  "Inner Join GlMast G On A.BrCd = G.BrCd And A.PrdCd = G.SubGlCode " +
                  "Inner Join GlMast G1 On A.BrCd = G1.BrCd And A.CGSTPrdCd = G1.SubGlCode " +
                  "Inner Join GlMast G2 On A.BrCd = G2.BrCd And A.SGSTPrdCd = G2.SubGlCode " +
                  "Where A.BrCd = '" + BrCode + "' And A.Stage <> 1004 ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}