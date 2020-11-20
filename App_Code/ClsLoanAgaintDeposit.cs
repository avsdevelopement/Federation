using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanAgaintDeposit
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", Res = "";
    int Result = 0, RM;
    double Balance;

	public ClsLoanAgaintDeposit()
	{
		
	}

    public string GetCustName(string BrCode, string CustNo)
    {
        try
        {
            sql = "Select (CustName +'_'+ ConVert(VarChar(10), ConVert(BigInt, IsNull(CustNo, 0))) +'_'+ ConVert(VarChar(10), ConVert(BigInt, IsNull(Group_CustNo, 0)))) CustName From Master Where CustNo = '" + CustNo + "'";
            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public DataTable GetCustName(string BRCD, string GLCODE, string ACCNO)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.BRCD='" + BRCD + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.ACCNO='" + ACCNO + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int BindGrid(GridView Gview, string BRCD, string CustNo)
    {
        try
        {
            //sql = "SELECT (CONVERT(VARCHAR(10),D.CUSTNO)+'_'+CONVERT(VARCHAR(10),D.CUSTACCNO)+'_'+CONVERT(VARCHAR(10),D.DEPOSITGLCODE)) AS ID, G.GLNAME, " +
            //      "D.CUSTNO, D.CUSTACCNO, D.DEPOSITGLCODE, D.PRNAMT, D.RATEOFINT, CONVERT(VARCHAR(10),D.OPENINGDATE, 103) AS OPENINGDATE, " +
            //      "CONVERT(VARCHAR(10),D.DUEDATE, 103) AS DUEDATE, D.PERIOD, D.INTAMT, D.MATURITYAMT, D.LIENMARK, D.LIENAMOUNT, D.LOANSUBGLCD, D.LOANACCNO " +
            //      "FROM DEPOSITINFO D " +
            //      "INNER JOIN AVS_ACC AC WITH(NOLOCK) ON AC.BRCD = D.BRCD AND AC.SUBGLCODE = D.DEPOSITGLCODE AND AC.ACCNO = D.CUSTACCNO " +
            //      "INNER JOIN GLMAST G WITH(NOLOCK) ON G.BRCD = AC.BRCD AND G.GLCODE = AC.GLCODE AND G.SUBGLCODE = AC.SUBGLCODE " +
            //      "WHERE D.BRCD  = '" + BRCD + "' AND D.CUSTNO = '" + CustNo + "' AND D.LMSTATUS = 1 AND D.STAGE = '1003' " +
            //      "Union " +
            //      "SELECT (CONVERT(VARCHAR(10),D.CUSTNO)+'_'+CONVERT(VARCHAR(10),D.CUSTACCNO)+'_'+CONVERT(VARCHAR(10),D.DEPOSITGLCODE)) AS ID, G.GLNAME, " +
            //      "D.CUSTNO, D.CUSTACCNO, D.DEPOSITGLCODE, D.PRNAMT, D.RATEOFINT, CONVERT(VARCHAR(10),D.OPENINGDATE, 103) AS OPENINGDATE, " +
            //      "CONVERT(VARCHAR(10),D.DUEDATE, 103) AS DUEDATE, D.PERIOD, D.INTAMT, D.MATURITYAMT, D.LIENMARK, D.LIENAMOUNT, D.LOANSUBGLCD, D.LOANACCNO " +
            //      "FROM DEPOSITINFO D " +
            //      "INNER JOIN AVS_ACC AC WITH(NOLOCK) ON AC.BRCD = D.BRCD AND AC.SUBGLCODE = D.DEPOSITGLCODE AND AC.ACCNO = D.CUSTACCNO " +
            //      "INNER JOIN GLMAST G WITH(NOLOCK) ON G.BRCD = AC.BRCD AND G.GLCODE = AC.GLCODE AND G.SUBGLCODE = AC.SUBGLCODE " +
            //      "WHERE D.BRCD = '" + BRCD + "' And D.CustNo In (Select CustNo From Master Where Group_CustNo = '" + CustNo + "') AND D.LMSTATUS = 1 AND D.STAGE = '1003'";

            //CHANGED BY ANKITA 05/03/2018 TO DISPLAY ALL BRCD DATA OF A CUSTOMER
            sql = "SELECT (CONVERT(VARCHAR(10),D.CUSTNO)+'_'+CONVERT(VARCHAR(10),D.CUSTACCNO)+'_'+CONVERT(VARCHAR(10),D.DEPOSITGLCODE)) AS ID, G.GLNAME, " +
                 "D.CUSTNO, D.CUSTACCNO, D.DEPOSITGLCODE, D.PRNAMT, D.RATEOFINT, CONVERT(VARCHAR(10),D.OPENINGDATE, 103) AS OPENINGDATE, " +
                 "CONVERT(VARCHAR(10),D.DUEDATE, 103) AS DUEDATE, D.PERIOD, D.INTAMT, D.MATURITYAMT, D.LIENMARK, D.LIENAMOUNT, D.LOANSUBGLCD, D.LOANACCNO " +
                 "FROM DEPOSITINFO D " +
                 "INNER JOIN AVS_ACC AC WITH(NOLOCK) ON AC.BRCD = D.BRCD AND AC.SUBGLCODE = D.DEPOSITGLCODE AND AC.ACCNO = D.CUSTACCNO " +
                 "INNER JOIN GLMAST G WITH(NOLOCK) ON G.BRCD = AC.BRCD AND G.GLCODE = AC.GLCODE AND G.SUBGLCODE = AC.SUBGLCODE " +
                 "WHERE D.CUSTNO = '" + CustNo + "' AND D.LMSTATUS = 1 AND D.STAGE = '1003' " +
                 "Union " +
                 "SELECT (CONVERT(VARCHAR(10),D.CUSTNO)+'_'+CONVERT(VARCHAR(10),D.CUSTACCNO)+'_'+CONVERT(VARCHAR(10),D.DEPOSITGLCODE)) AS ID, G.GLNAME, " +
                 "D.CUSTNO, D.CUSTACCNO, D.DEPOSITGLCODE, D.PRNAMT, D.RATEOFINT, CONVERT(VARCHAR(10),D.OPENINGDATE, 103) AS OPENINGDATE, " +
                 "CONVERT(VARCHAR(10),D.DUEDATE, 103) AS DUEDATE, D.PERIOD, D.INTAMT, D.MATURITYAMT, D.LIENMARK, D.LIENAMOUNT, D.LOANSUBGLCD, D.LOANACCNO " +
                 "FROM DEPOSITINFO D " +
                 "INNER JOIN AVS_ACC AC WITH(NOLOCK) ON AC.BRCD = D.BRCD AND AC.SUBGLCODE = D.DEPOSITGLCODE AND AC.ACCNO = D.CUSTACCNO " +
                 "INNER JOIN GLMAST G WITH(NOLOCK) ON G.BRCD = AC.BRCD AND G.GLCODE = AC.GLCODE AND G.SUBGLCODE = AC.SUBGLCODE " +
                 "WHERE  D.CustNo In (Select CustNo From Master Where Group_CustNo = '" + CustNo + "') AND D.LMSTATUS = 1 AND D.STAGE = '1003'";
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

    public string ChkAccStatus(string BrCode, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Acc_Status From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "'";
            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Res = "";
        }
        return Res;
    }

    public int CheckExist(string BRCD, string PT, string AC)
    {
        try
        {
            sql = "Select 1 From LoanInfo Where BrCd ='" + BRCD + "' And LoanGlCode = '" + PT + "' And CustAccno ='" + AC + "' And Stage <> 1004 And LmStatus = 1";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                Result = -1;
            }
            else
            {
                Result = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return -1;
        }
        return Result;
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

                sql="Exec SP_LoanAgainstDeposit @BrCode = '"+BrCode+"', @DepCustNo = '"+DepCustNo+"', @DepGlCode = '"+DepSubGl+"', @DepAccNo = '"+DepAccNo+"', @DepBalance = '"+DepAmount+"', @LoanSancAmt = '"+LoanAmount+"', @RateOfInt = '"+IntRate+"', @Period = '"+Period+"', @Installment = '"+Installment+"', @SancDate = '"+conn.ConvertDate(SancDate).ToString()+"', @DueDate = '"+conn.ConvertDate(DueDate).ToString()+"', @Remark1 = '"+Remark1+"', @Remark2 = '"+Remark2+"', @LoanSubGl = '"+LoanSubGl+"', @LoanAccNo = '"+LoanaccNo+"', @MID = '"+Mid+"', @PCMAC = '"+conn.PCNAME().ToString()+"', @EDate = '"+conn.ConvertDate(EDate).ToString()+"', @Flag = 'MLien' ";
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

    public int LienDetailsInsert(string BrCode, double LoanAmount, double IntRate, string Period, double Installment, string SancDate, string DueDate, string Remark1, string Remark2, string LoanCustNo, string LoanSubGl, string LoanaccNo, string Mid, string EDate)
    {
        try
        {
            sql = "Exec SP_LoanAgainstDeposit @BrCode = '" + BrCode + "', @LoanSancAmt = '" + LoanAmount + "', @RateOfInt = '" + IntRate + "', @Period = '" + Period + "', @Installment = '" + Installment + "', @SancDate = '" + conn.ConvertDate(SancDate).ToString() + "', @DueDate = '" + conn.ConvertDate(DueDate).ToString() + "', @Remark1 = '" + Remark1 + "', @Remark2 = '" + Remark2 + "', @LoanCustNo = '"+ LoanCustNo +"', @LoanSubGl = '" + LoanSubGl + "', @LoanAccNo = '" + LoanaccNo + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'Lien' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
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
}