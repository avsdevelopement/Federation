using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsInvData
/// </summary>
public class ClsInvData
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    double ODAmount = 0;
    int Result = 0;

	public ClsInvData()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetProductName(string BrCode, string ProdCode)
    {
        try
        {
            sql = "Select (IsNull(GlName, '') +'_'+ ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode)) As Name " +
                  "From GlMast Where BrCd = '" + BrCode + "' And SubGlCode ='" + ProdCode + "' ";
            sResult = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GetAccountName(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select (M.BankName+'_'+ ConVert(VarChar(10), ConVert(BigInt, M.CustAccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, M.CustAccNo))) As Name " +
                  "From AVS_InvAccountMaster M With(NoLock) " +
                  "Where M.BrCd = '" + BrCode + "' And M.SubGlCode = '" + ProdCode + "' And M.CustAccNo = '" + AccNo + "' And M.Stage <> '1004'";
            sResult = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GetAccDetails(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select (ConVert(VarChar(10), A.OpeningDate, 103) +'_'+ ConVert(VarChar(10), L.DESCRIPTIONMAR)) As Name " +
                  "From AVS_InvAccountMaster A With(NoLock) Inner Join Lookupform1 L On L.Srno = A.AccStatus And L.Lno='1052' Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + ProdCode + "' And A.CustAccNo = '" + AccNo + "' And A.Stage <> '1004'";
            sResult = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public DataTable GetStatementView(string BrCode, string GlCode, string PrCode, string AccNo, string Recno, string FDate, string TDate)
    {
        try
        {
            DT = new DataTable();
            sql = "Exec rptInvAccStatement '" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + AccNo + "', '" + Conn.ConvertDate(FDate).ToString() + "', '" + Conn.ConvertDate(TDate).ToString() + "'";
            DT = Conn.GetDatatable(sql);
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
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + Conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(Conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }
    public DataTable AccountInfo(string BrCode, string GlCode, string ProdCode, string AccNo)
    {
        try
        {
            DT = new DataTable();
            sql = "Select PrincipleAmt As PrnAmt, RateOfInt As RateOfInt, OpeningDate, Period As Period, 0 As MaturityAmt From Avs_InvDepositeMaster Where BrCd = '" + BrCode + "' And Subglcode = '" + ProdCode + "' And CustAccno = '" + AccNo + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
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

    public DataSet GetStmtView(string BrCode, string GlCode, string PrCode, string AccNo, string FDate, string TDate)
    {
        try
        {
            sql = "Exec rptInvAccStatement '" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + AccNo + "', '" + Conn.ConvertDate(FDate).ToString() + "', '" + Conn.ConvertDate(TDate).ToString() + "'";
            DT = new DataTable();
            DS = new DataSet();
            DT = Conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
}