using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanAgaintDDS
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", Result = "";
    int IntResult = 0;

	public ClsLoanAgaintDDS()
	{
		
	}

    public string Getcustname(string custno, string BRCD)
    {
        try
        {
            sql = "SELECT (CUSTNAME+'_'+Convert(VARCHAR(10),CUSTNO)) CUSTNAME FROM MASTER WHERE CUSTNO='" + custno + "'";
            custno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }

    public string GetAccNo(string AT, string BRCD)
    {
        try
        {
            sql = "Select (Convert(VarChar(10), GlCode)) +'_'+ Glname From GlMast Where BrCd = '" + BRCD + "' And SubGlCode = '" + AT + "' Group By GlCode, GlName";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public string GetLoanAccNo(string AT, string BRCD)
    {
        try
        {
            sql = "Select (Convert(VarChar(10), GlCode)) +'_'+ Glname From GlMast Where BrCd = '" + BRCD + "' And GlGroup = 'LNV' And SubGlCode = '" + AT + "' Group By GlCode, GlName";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string BRCD, string GLCODE, string ACCNO)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.BRCD='" + BRCD + "' And AC.SUBGLCODE='" + GLCODE + "' And AC.ACCNO='" + ACCNO + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int BindGrid(GridView Gview, string BRCD, string CustNo, string EDate)
    {
        try
        {
            sql = "Exec SP_LoanAgainstDDSBal '" + BRCD + "','" + CustNo + "','" + conn.ConvertDate(EDate).ToString() + "'";

            IntResult = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }

    public int GetMaxLimit(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select IsNull(DepositGlBalance, 0) From DepositGl Where BrCd = '" + BrCode + "' and DepositGlCode = '" + SubGlCode + "'";
            IntResult = Convert.ToInt32(conn.sExecuteScalar(sql));

            if (IntResult == 0)
            {
                IntResult = Convert.ToInt32(95);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return IntResult = 0;
        }
        return Convert.ToInt32(IntResult);
    }

    public string ChkAccStatus(string BrCode, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Acc_Status From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = "";
        }
        return Result;
    }

    public string GetGlCodeAndName(string BrCode)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(10), LOANGLCODE) +'_'+ CONVERT(VARCHAR(70), LOANTYPE) AS LOANGLNAME FROM LOANGL WHERE BRCD = '" + BrCode + "' AND LOANCATEGORY = 'LAD' ";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public string GETBONDNO(string BrCode)
    {
        try
        {
            sql = "Select IsNull(LastNo, 0)+1 From avs1000 Where TYPE = 'BONDNO' And ACTIVITYNO = (Select SubGlCode From GlMast Where BrCd = '" + BrCode + "' And GlGroup = 'LAD') And BRCD='" + BrCode + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetDetails(GridView GView,string BrCode, string AgCode, string AccNo)
    {
        try
        {
            sql = "Select * From LoanInfo Where BrCd = '" + BrCode + "' And LoanGlCode = '" + AgCode + "' And CustAccNo = '" + AccNo + "'";
            IntResult = conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
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

    public double GetBalance(string BrCode, string AgCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Exec SP_OpClBalance '" + BrCode + "','" + AgCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "','ClBal'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(Result);
    }

    public int CheckExist(string BRCD, string PT, string AC)
    {
        try
        {
            sql = "Select 1 From LoanInfo Where BrCd ='" + BRCD + "' And LoanGlCode ='" + PT + "' And CustAccNo ='" + AC + "' And Stage <> 1004 And LmStatus = 1";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                IntResult = -1;
            }
            else
            {
                IntResult = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return -1;
        }
        return IntResult;
    }

    public int LienMarkInsert(string BrCode, string Custaccgl, double DepAmount, double LoanAmount, double IntRate, string Period, double Installment, string SancDate, string DueDate, string Remark1, string Remark2, string LoanSubGl, string LoanaccNo, string Mid, string EDate)
    {
        string DepCustNo, DepSubGl, DepAccNo;
        try
        {
            string[] custnob = Custaccgl.Split('_');
            if (custnob.Length > 1)
            {
                DepCustNo = custnob[0].ToString();
                DepAccNo = custnob[1].ToString();
                DepSubGl = custnob[2].ToString();

                sql = "Exec SP_LoanAgainstDDS @BrCode = '" + BrCode + "', @AgentCustNo = '" + DepCustNo + "', @AgentCode = '" + DepSubGl + "', @AgentAccNo = '" + DepAccNo + "', @DepBalance = '" + DepAmount + "', @LoanSancAmt = '" + LoanAmount + "', @RateOfInt = '" + IntRate + "', @Period = '" + Period + "', @Installment = '" + Installment + "', @SancDate = '" + conn.ConvertDate(SancDate).ToString() + "', @DueDate = '" + conn.ConvertDate(DueDate).ToString() + "', @Remark1 = '" + Remark1 + "', @Remark2 = '" + Remark2 + "', @LoanSubGl = '" + LoanSubGl + "', @LoanAccNo = '" + LoanaccNo + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'MLien'";
                IntResult = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }

    public int LienDetailsInsert(string BrCode, double LoanAmount, double IntRate, string Period, double Installment, string SancDate, string DueDate, string Remark1, string Remark2, string LoanCustNo, string LoanSubGl, string LoanaccNo, string Mid, string EDate)
    {
        try
        {
            sql = "Exec SP_LoanAgainstDDS @BrCode = '" + BrCode + "', @LoanSancAmt = '" + LoanAmount + "', @RateOfInt = '" + IntRate + "', @Period = '" + Period + "', @Installment = '" + Installment + "', @SancDate = '" + conn.ConvertDate(SancDate).ToString() + "', @DueDate = '" + conn.ConvertDate(DueDate).ToString() + "', @Remark1 = '" + Remark1 + "', @Remark2 = '" + Remark2 + "', @LoanCustNo = '" + LoanCustNo + "', @LoanSubGl = '" + LoanSubGl + "', @LoanAccNo = '" + LoanaccNo + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'Lien'";
            IntResult = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }
}