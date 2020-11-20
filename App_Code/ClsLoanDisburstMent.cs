using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanDisburstMent
{
    DbConnection conn = new DbConnection();
    ClsAuthorized AZ = new ClsAuthorized();
    ClsOpenClose OC = new ClsOpenClose();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    string sql = "", SResult = "";
    int Result = 0;

    public ClsLoanDisburstMent()
    {
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

    public string GetAccStatus(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + BrCode + "' AND SUBGLCODE='" + PrCode + "' AND ACCNO='" + AccNo + "' And Stage = '1003'";
            SResult = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return SResult;
    }

    public int InsertIntoTable(string Edate, string BrCode, string CustNo, string CustName, string GlCode, string SubGlCode, string AccNo, string Parti1, string Parti2, string Amount, string TrxType, string Activity, string PmtMode, string InstNo, string InstDate, string Mid)
    {
        try
        {
            sql = "Insert Into Avs_TempLoanDisb (EntryDate, BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, InstNo, InstDate, Mid) " +
                  "Values ('" + conn.ConvertDate(Edate).ToString() + "','" + BrCode + "','" + CustNo + "', '" + CustName + "', '" + GlCode + "','" + SubGlCode + "','" + AccNo + "','" + Parti1 + "','" + Parti2 + "','" + Amount + "','" + TrxType + "','" + Activity + "','" + PmtMode + "','" + InstNo + "', '" + conn.ConvertDate(InstDate).ToString() + "','" + Mid + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DelRecTable(string id, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From Avs_TempLoanDisb Where ID = '" + id + "' And BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
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
            sql = "Delete From Avs_TempLoanDisb Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetCreditAmt(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select Abs(IsNull(Sum(A.Credit), 0)) As Credit From ( " +
                  "Select (Case When TrxType = '1' Then Sum(Amount) Else '0' End) As Credit From Avs_TempLoanDisb " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' " +
                  "Group By TrxType)A ";

            DT = new DataTable();
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
            sql = "Select ID, SubGlCode, AccNo, CustName, Amount, Particulars2, (Case When TRXTYPE = 1 Then 'CR' Else 'DR' End) As TrxType From Avs_TempLoanDisb "+
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' Order By ID";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetTransDetails(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select ID, EntryDate, BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, InstNo, Convert(VarChar(10), InstDate, 103) As InstDate, Mid From Avs_TempLoanDisb " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
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

    public int InsertTemp(string PT, string GLNAME, string AC, string ACNAME, string CUSTNO, string AMT, string NR, string MID, string MID_DATE, string BRCD, string DPT, string DACC, string GLCCD)
    {
        try
        {
            sql = "INSERT INTO LOANDISBURST_H (SUBGLCODE,GLNAME,ACCNO,ACCNAME,CUSTNO,AMT,NARRATION,MID,MID_DATE,SYSTEMDATE,BRCD,DPRDCD,DACCNO,GLCD) " +
                "VALUES('" + PT + "','" + GLNAME + "','" + AC + "','" + ACNAME + "','" + CUSTNO + "','" + AMT + "','" + NR + "','" + MID + "','" + conn.ConvertDate(MID_DATE).ToString() + "','" + conn.ConvertDate(DateTime.Now.Date.ToString("dd/MM/yyyy")).ToString() + "','" + BRCD + "','" + DPT + "','" + DACC + "','" + GLCCD + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetLoancat(string BrCode, string SubGlCode, string AccNo)
    {
        try
        {
            string sql = "Select LOANCATEGORY From loanGl Where BrCd = '" + BrCode + "' And LoanGlCode = '" + SubGlCode + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public int DeleteTemp(string MID, string BRCD, string MID_DATE, string PT, string AC)
    {
        sql = "DELETE FROM LOANDISBURST_H WHERE MID='" + MID + "' AND MID_DATE='" + conn.ConvertDate(MID_DATE).ToString() + "' AND BRCD='" + BRCD + "' AND DPRDCD='" + PT + "' AND DACCNO='" + AC + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }

    public int BindTemp(GridView Gview, string MID, string MID_Date, string BRCD, string PT, string AC)
    {
        try
        {
            sql = "SELECT * FROM LOANDISBURST_H WHERE MID='" + MID + "' AND MID_DATE='" + conn.ConvertDate(MID_Date) + "' AND BRCD='" + BRCD + "' AND DPRDCD='" + PT + "' AND DACCNO='" + AC + "' ";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetDisburstAMT(string PrCode, string AccNo, string EDate, string BrCode)
    {
        try
        {
            //sql = "SELECT LIMIT,CUSTNO FROM LOANINFO WHERE LOANGLCODE='" + PrCode + "' AND CUSTACCNO='" + AccNo + "' AND BRCD='" + BrCode + "' AND LMSTATUS='1'";
            sql = "Select L.Limit, L.Period, ConVert(VarChar(10), A.OpeningDate, 103) As SancDate, L.CustNo, L.Installment, L.IntRate, ConVert(VarChar(10), L.DueDate, 103) As DueDate " +
                  "From LoanInfo L Inner Join Avs_Acc A With(NoLock) On A.BRCD = L.BRCD And A.SubGlCode = L.LoanGlCode And A.AccNo = L.CustAccNo " +
                  "Where L.BRCD = '" + BrCode + "' AND L.LoanGlCode = '" + PrCode + "' AND L.CustAccNo = '" + AccNo + "' AND L.LmStatus = '1'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public double GetAMT(string MID, string MID_Date, string BRCD, string DPT, string DACC)
    {
        double AMT = 0;
        try
        {
            sql = "SELECT ISNULL(SUM(AMT),0) FROM LOANDISBURST_H WHERE MID='" + MID + "' AND MID_DATE='" + MID_Date + "' AND BRCD='" + BRCD + "' AND DPRDCD='" + DPT + "' AND DACCNO='" + DACC + "'";
            AMT = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AMT;
    }

    public int UpdateStatus(string BrCode, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Update LoanInfo SET DisYn = '2' Where BrCd = '" + BrCode + "' And LoanGlCode = '" + SubGlCode + "' And CustAccNo = '" + AccNo + "' And LmStatus = '1' And DisYn = '1'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string CheckGlGroup(string BRCD, string PrCode)
    {
        string RC = "";
        try
        {
            string sql = "SELECT ISNULL(GLGROUP, '') FROM GLMAST WHERE BRCD = '" + BRCD + "' and SUBGLCODE='" + PrCode + "'";
            RC = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
        return RC;
    }

    public string CheckAccount(string AC, string PT, string BRCD)
    {
        try
        {
            try
            {
                sql = "SELECT CUSTNO FROM AVS_ACC WHERE SUBGLCODE='" + PT + "' AND ACCNO='" + AC + "' AND BRCD='" + BRCD + "'";
                SResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SResult;

    }

    public int CheckRecords(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "SELECT TOP 1 1 FROM AVS_GLDORATable WHERE BRCD = '" + BrCode + "' AND SUBGLCODE = '" + PrCode + "' AND ACCNO = '" + AccNo + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Convert.ToInt32(0);
        }
        return Convert.ToInt32(BrCode);
    }
}