using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public class ClsAVS5036
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAVS5036()
	{
		
	}

    public DataSet GetSBTORDCal(string BrCode, string AsOnDate, string WorkDate, string MID, string Flag1, string Flag2)
    {
        try
        {
            sql = "Exec SP_AVS5036 '" + BrCode + "', '" + conn.ConvertDate(AsOnDate).ToString() + "', '" + conn.ConvertDate(WorkDate).ToString() + "', '" + MID + "', '" + Flag1 + "', '" + Flag2 + "'";
            DT = new DataTable();
            DS = new DataSet();
            DT = conn.GetDatatable(sql);

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

    public string SIPost(string BrCode, string AsOnDate, string WorkDate, string MID, string Flag1, string Flag2)
    {
        try
        {
            sql = "Exec SP_AVS5036 '" + BrCode + "', '" + conn.ConvertDate(AsOnDate).ToString() + "', '" + conn.ConvertDate(WorkDate).ToString() + "', '" + MID + "', '" + Flag1 + "', '" + Flag2 + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetSIPostData(string BrCode, string AsOnDate, string WorkDate, string MID, string Flag1, string Flag2)
    {
        try
        {
            sql = "Exec SP_AVS5036 '" + BrCode + "', '" + conn.ConvertDate(AsOnDate).ToString() + "', '" + conn.ConvertDate(WorkDate).ToString() + "', '" + MID + "', '" + Flag1 + "', '" + Flag2 + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double GetOtherIntRate(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select OTHERCHG From LOANGL Where BrCd = '" + BrCode + "' and LoanGlCode='" + SubGlCode + "'";
            sResult = conn.sExecuteScalar(sql);
            if (sResult == null)
                sResult = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sResult == "" ? "0.00" : sResult);
    }

    public DataTable GetLoanTotalAmount(string BrCode, string PrCode, string AccNo, string EDate, string FL = "CL") // Optional Parameter added by Abhishek for Pen Requirement 04/09/2017
    {
        try
        {
            sql = "Exec RptAllLoanBalances '" + BrCode + "','" + PrCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "','LoanInst','" + FL + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int UpdateNextShedule(string BrCode, string PrCode, string AccNo, string EDate, string WorkDate)
    {
        try
        {
            sql = "Update AVS5007 Set NEXTEXECUTIONDATE = DateAdd(MM, 1, NEXTEXECUTIONDATE), LASTEXECUTDATE = '" + conn.ConvertDate(EDate).ToString() + "', LASTPOSTINGDATE = '" + conn.ConvertDate(WorkDate).ToString() + "' Where BRCD = '" + BrCode + "' And CRSUBGL = '" + PrCode + "' And CRACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}