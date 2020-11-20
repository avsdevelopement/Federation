using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ClsStatementView
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    double ODAmount = 0;
    int Result = 0;

	public ClsStatementView()
	{
		
	}

    public DataTable GetBankName(string BrCode)
    {
        try
        {
            DT = new DataTable();
            sql = "Select BankName, MidName As BranchName From BankName Where BrCd = '" + BrCode + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetProductName(string BrCode, string ProdCode)
    {
        try
        {
            sql = "Select (IsNull(GlName, '') +'_'+ ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode)) As Name " +
                  "From GlMast Where BrCd = '" + BrCode + "' And SubGlCode ='" + ProdCode + "'";
            sResult = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetAccYN(string BrCode, string ProdCode)
    {
        try
        {
            sql = "Select IsNull(IntAccYn, '') As Name From GlMast Where BrCd = '" + BrCode + "' And SubGlCode ='" + ProdCode + "'";
            sResult = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }



    public DataTable GetStatementView(string BrCode, string GlCode, string PrCode, string AccNo, string RecNo, string FDate, string TDate)
    {
        try
        {
            DT = new DataTable();
            sql = "Exec rptAccStatementView '" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + AccNo + "', '" + RecNo + "','" + Conn.ConvertDate(FDate).ToString() + "', '" + Conn.ConvertDate(TDate).ToString() + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetAccountName(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo))) As Name " +
                  "From Master M With(NoLock) " +
                  "Inner Join Avs_Acc A With(NoLock) On A.CustNo = M.CustNo " +
                  "Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + ProdCode + "' And A.AccNo = '" + AccNo + "' And A.Stage <> '1004'";
            sResult = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable AccountInfo(string BrCode, string GlCode, string ProdCode, string AccNo)
    {
        try
        {
            DT = new DataTable();
            if (GlCode.ToString() == "3")
                sql = "Select Limit, IntRate, SanssionDate, Period, InstallMent From LoanInfo Where BrCd = '" + BrCode + "' And LoanGlCode = '" + ProdCode + "' And CustAccno = '" + AccNo + "'";
            if (GlCode.ToString() == "5")
                sql = "Select PrnAmt, RateOfInt, OpeningDate, Period, IntAmt, MaturityAmt,format(DueDate,'dd/MM/yyyy') DueDate From DepositInfo Where BrCd = '" + BrCode + "' And DepositGlCode = '" + ProdCode + "' And CustAccno = '" + AccNo + "'";

            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double OVerdueAmt(string BrCode, string ProdCode, string AccNo, double Double, string EDate)
    {
        try
        {
            sql = "Select dbo.FncODBalance ('" + BrCode + "', '" + ProdCode + "', '" + AccNo + "', '" + Double + "', '" + Conn.ConvertDate(EDate).ToString() + "')";
            ODAmount = Convert.ToDouble(Conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ODAmount;
    }

    public string GetAccDetails(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select (ConVert(VarChar(10), A.OpeningDate, 103) +'_'+ ConVert(VarChar(05), A.Acc_Status)) As Name " +
                  "From Avs_Acc A With(NoLock) Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + ProdCode + "' And A.AccNo = '" + AccNo + "' And A.Stage <> '1004'";
            sResult = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + Conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(Conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public DataTable GetStatementView(string BrCode, string GlCode, string PrCode, string AccNo, string FDate, string TDate)
    {
        try
        {
            DT = new DataTable();
            sql = "Exec rptAccStatementView '" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + AccNo + "', '" + Conn.ConvertDate(FDate).ToString() + "', '" + Conn.ConvertDate(TDate).ToString() + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

   

    public DataTable MonthlyAccStat(string BRCD, string SUBGL, string ACCNO, string FDATE, string TDATE)
    {
        try
        {
            DT = new DataTable();
            // sql = "Exec SP_ACCSTATUS @pfmonth='"+PFMONTH+"',@ptmonth='"+PTMONTH+"',@PFDT='"+conn.ConvertDate(PFDT)+"',@PTDT='"+conn.ConvertDate(PTDT)+"',@pfyear='"+PFYEAR+"',@ptyear='"+PTYEAR+"',@pac='"+PAC+"',@pat='"+PAT+"'";
            sql = "EXEC monthlystatment @Brcd='" + BRCD + "',@SubGlCode='" + SUBGL + "',@Accno='" + ACCNO + "',@FromDate='" + Conn.ConvertDate(FDATE) + "',@ToDate='" + Conn.ConvertDate(TDATE) + "'";
            DT = new DataTable(sql);
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

    public DataTable GetVoucherDetails(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string TableName;
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            DT = new DataTable();
            sql = "Select ConVert(VarChar(10), A.EntryDate, 103) As EntryDate, A.SetNo, A.GlCode, A.SubGlCode, A.AccNo, Concat(A.Particulars, ' ', A.Particulars2) As Parti, " +
                  "A.InstrumentNo As InstNo, ConVert(VarChar(10), A.InstrumentDate, 103) As InstDate, " +
                  "(Case When A.TrxType = '1' Then A.Amount Else 0 End) As Credit, (Case When A.TrxType = '2' Then A.Amount Else 0 End) As Debit, A.Stage " +
                  "From " + TableName + " A With(NoLock) Where A.BrCd = '" + BrCode + "' And A.EntryDate = '" + Conn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' ";
            DT = new DataTable(sql);
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

}