using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsLoanBscReport
/// </summary>
public class ClsLoanBscReport
{
    int Disp;
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
    public ClsLoanBscReport()
    {

    }
    public int GetFilter(GridView grid, string FL, string FBRCD, string TBRCD, string FPRDTYPE, string TPRDTYPE, string FACTYPE, string TACTYPE)
    {
        try
        {
            if (FL == "BRPDAC")
            {
                sql = "EXEC SP_LOANBASIC @flag='" + FL + "',@FBRCD='" + FBRCD + "',@TBRCD ='" + TBRCD + "',@FPRD='" + FPRDTYPE + "',@TPRD='" + TPRDTYPE + "',@FAC	='" + FACTYPE + "',@TAC='" + TACTYPE + "'";
            }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Disp;
    }
    public DataTable GetLoanBInfo(string FL, string FBRCD, string TBRCD, string FPRDTYPE, string TPRDTYPE, string FACTYPE, string TACTYPE)
    {
        DataTable DT1 = new DataTable();
        try
        {
             sql = "EXEC SP_LOANBASIC @flag='" + FL + "',@FBRCD='" + FBRCD + "',@TBRCD ='" + TBRCD + "',@FPRD='" + FPRDTYPE + "',@TPRD='" + TPRDTYPE + "',@FAC	='" + FACTYPE + "',@TAC='" + TACTYPE + "'"; 
            DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }
    public DataTable DIsplayChequeDet(string MembNo, string FL)
    {
        try
        {
            sql = "Exec SP_MemDivDataGet @RefID='" + FL + "',@MemID='" + MembNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetChequeMsebS(string MemberNo, string Divident, string DepositInt, string TotalPay, string CheqNo, string BankCode, string FL, string Flag)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "EXEC SP_MemDivDataPost @MemID ='" + MemberNo + "',@Shr_Int='" + Divident + "',@DP_Int='" + DepositInt + "',@Tot_Int='" + TotalPay + "',@ChequeNO='" + CheqNo + "',@DrBank='" + BankCode + "',@RefID='" + FL + "',@Flag='" + Flag + "'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable SplitEdateS(string Edate)
    {
        try
        {
            sql = "Select Right('" + Edate + "',1)as Y4, Right(Substring('" + Edate + "',8,2),1)as Y3, Right(Substring('" + Edate + "',8,1),1)as Y2, Right(Substring('" + Edate + "',7,1),1)as Y1, " +
                " Right(Substring('" + Edate + "',5,1),1)as M2, Right(Substring('" + Edate + "',4,1),1)as M1, Right(Substring('" + Edate + "',2,1),1)as D2, Right(Substring('" + Edate + "',1,1),1)as D1";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }


    public string GetChequeMsebS_P(string MemberNo, string Divident, string DepositInt, string TotalPay, string CheqNo, string BankCode, string FL, string Flag)
    {
        try
        {
            sql = "EXEC SP_MemDivDataPost @MemID ='" + MemberNo + "',@Shr_Int='" + Divident + "',@DP_Int='" + DepositInt + "',@Tot_Int='" + TotalPay + "',@ChequeNO='" + CheqNo + "',@DrBank='" + BankCode + "',@RefID='" + FL + "',@Flag='" + Flag + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetChequeMsebS_CrP(string Brcd, string MemberNo, string Divident, string DepositInt, string TotalPay, string CheqNo, string BankCode, string FL, string Flag)
    {
        try
        {
            sql = "EXEC SP_MemDivDataCreditPost @BrCode ='" + Brcd + "',@MemID ='" + MemberNo + "',@Shr_Int='" + Divident + "',@DP_Int='" + DepositInt + "',@Tot_Int='" + TotalPay + "',@ChequeNO='" + CheqNo + "',@DrBank='" + BankCode + "',@RefID='" + FL + "',@Flag='" + Flag + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
}