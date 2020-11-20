using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsFDLoanAndLeanMark
{
    DbConnection conn = new DbConnection();
    string sql = "", Res = "";
    int Result = 0, RM;
    double Balance;

	public ClsFDLoanAndLeanMark()
	{
		
	}

    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetAccName(string AccT, string AccNo, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME,AC.OPENINGDATE FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + AccNo + "' AND AC.SUBGLCODE='" + AccT + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string Getcustname(string custno, string BRCD)
    {
        try
        {
            sql = "SELECT (CUSTNAME+'_'+Convert(VARCHAR(10),CUSTNO)) CUSTNAME FROM MASTER WHERE CUSTNO='" + custno + "' and brcd='" + BRCD + "'";
            custno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }

    public int BindGrid(GridView Gview, string BRCD ,string CustNo)
    {
        try
        {
            sql = "SELECT (CONVERT(VARCHAR(10),D.CUSTNO)+'_'+CONVERT(VARCHAR(10),D.CUSTACCNO)+'_'+CONVERT(VARCHAR(10),D.DEPOSITGLCODE)) AS ID, G.GLNAME, " +
                  "D.CUSTNO, D.CUSTACCNO, D.DEPOSITGLCODE, D.PRNAMT, D.RATEOFINT, CONVERT(VARCHAR(10),D.OPENINGDATE, 121) AS OPENINGDATE, " +
                  "CONVERT(VARCHAR(10),D.DUEDATE, 121) AS DUEDATE, D.PERIOD, D.INTAMT, D.MATURITYAMT, D.LIENMARK, D.LIENAMOUNT, D.LOANSUBGLCD, D.LOANACCNO " +
                  "FROM DEPOSITINFO D " +
                  "INNER JOIN AVS_ACC AC WITH(NOLOCK) ON AC.BRCD = D.BRCD AND AC.SUBGLCODE = D.DEPOSITGLCODE AND AC.ACCNO = D.CUSTACCNO " +
                  "INNER JOIN GLMAST G WITH(NOLOCK) ON G.BRCD = AC.BRCD AND G.GLCODE = AC.GLCODE AND G.SUBGLCODE = AC.SUBGLCODE " +
                  "WHERE D.BRCD = '" + BRCD + "' AND D.CUSTNO = '" + CustNo + "' AND D.LMSTATUS = 1 AND D.STAGE = '1003'";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindGridLien(GridView Gview, string BRCD, string CustNo, string AccNo, string GlCode)
    {
        try
        {
            sql = "SELECT * FROM DEPOSITINFO WHERE BRCD = '" + BRCD + "' and CUSTNO = '" + CustNo + "' and CUSTACCNO = '" + AccNo + "' AND DEPOSITGLCODE = '" + GlCode + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string BRCD, string CustNo, string AccNo, string GlCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT * FROM DEPOSITINFO WHERE BRCD = '" + BRCD + "' and CUSTNO = '" + CustNo + "' and CUSTACCNO = '" + AccNo + "' AND DEPOSITGLCODE = '" + GlCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public int FDLeanMarkInsert(string BrCode, string Custaccgl ,double DepAmount, string LoanGlCode, string LoanAccNo, string DisbDate, string Mid, string EDate)
    {
        string CUST, ACC, GL;
        try
        {
            string[] custnob = Custaccgl.Split('_');
            if (custnob.Length > 1)
            {
                CUST = custnob[0].ToString();
                ACC = custnob[1].ToString();
                GL = custnob[2].ToString();

                sql = "Exec SP_LoanAgainstDeposit @BrCode = '" + BrCode + "', @DepCustNo = '" + CUST + "', @DepAccNo = '" + ACC + "', @DepGlCode = '" + GL + "', @LnAmount = '" + DepAmount + "', @DisbDate = '" + conn.ConvertDate(DisbDate).ToString() + "', @LnGlCode = '" + LoanGlCode + "', @LnAccNo = '" + LoanAccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "'";
                RM = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RM = 0;
        }
        return RM;
    }

    public int GetMaxLimit(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select IsNull(DepositGlBalance, 0) From DepositGl Where BrCd = '" + BrCode + "' and DepositGlCode = '" + SubGlCode + "'";
            RM = Convert.ToInt32(conn.sExecuteScalar(sql));

            if (RM == 0)
            {
                RM = Convert.ToInt32(95);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RM = 0;
        }
        return Convert.ToInt32(RM);
    }

    public string GetCategory(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select IsNull(Category, '') From DepositGl Where BrCd = '" + BrCode + "' and DepositGlCode = '" + SubGlCode + "'";
            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Res = "";
        }
        return Res;
    }

    public double GetOpClBal(string BrCode, string SubGlCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Exec SP_OpClBalance '" + BrCode + "','" + SubGlCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "','OpBal'";
            Balance = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Balance = 0;
        }
        return Convert.ToDouble(Balance);
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
}