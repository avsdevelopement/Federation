using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for ClsLoanOverdue
/// </summary>
public class ClsLoanOverdue
{
    string sql = "";
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    int Result = 0;
    public ClsLoanOverdue()
    {

    }

    public DataTable GetLoanOverdue(string EDT, string BRCD, string DATE, string SUBGL, string FL)
    {
        try
        {
            if (FL == "OD")
            {
                sql = "EXEC SP_LOAN_OVERDUE @ONDATE='" + conn.ConvertDate(DATE) + "',@ACCTYPE='" + SUBGL + "',@BRCD='" + BRCD + "',@FLAG='OD'";
            }
            else if (FL == "NPA")
            {
                sql = "EXEC SP_LOAN_OVERDUE @ONDATE='" + conn.ConvertDate(DATE) + "',@ACCTYPE='" + SUBGL + "',@BRCD='" + BRCD + "',@FLAG='NPA'";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetLoanOverdue_New1(string EDT, string BRCD, string DATE, string SUBGL, string FLL, string SFL)//Dhanya Shetty for dashboard report
    {
        try
        {
            sql = "EXEC ISP_DashLoanOverdue @Flag='OD',@SFlag='" + SFL + "',@Brcd='" + BRCD + "',@Sbgl='" + SUBGL + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetLoanOverdue_New(string EDT, string FBRCD, string TBRCD, string DATE, string FSUBGL, string TSUBGL, string FL, string SFL)
    {
        try
        {
            if (FL == "OD")
            {
                //sql = "EXEC SP_LOAN_OVERDUE @ONDATE='" + conn.ConvertDate(DATE) + "',@ACCTYPE='" + SUBGL + "',@BRCD='" + BRCD + "',@FLAG='OD'";
                //sql = "EXEC ISP_LoanOverdue @Flag='OD',@SFlag='" + SFL + "',@Brcd='" + BRCD + "',@Sbgl='" + SUBGL + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
                sql = "Exec ISP_LoanOverdue  @Flag='OD',@SFlag='" + SFL + "',@FSbgl='" + FSUBGL + "',@TSbgl='" + TSUBGL + "',@FBrcd='" + FBRCD + "',@TBrcd='" + TBRCD + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
            }
            else if (FL == "NPA")
            {
                //sql = "EXEC SP_LOAN_OVERDUE @ONDATE='" + conn.ConvertDate(DATE) + "',@ACCTYPE='" + SUBGL + "',@BRCD='" + BRCD + "',@FLAG='NPA'";
                //  sql = "EXEC ISP_LoanOverdue @Flag='NPA',@SFlag='" + SFL + "',@Brcd='" + BRCD + "',@Sbgl='" + SUBGL + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataSet GetLoanBalanceListPen(string EDT, string FBRCD, string TBRCD, string DATE, string FSUBGL, string TSUBGL, string FL, string SFL)
    {
        try
        {

            sql = "Exec ISP_LoanBalanceList_Pen  @Flag='OD',@SFlag='" + SFL + "',@FSbgl='" + FSUBGL + "',@TSbgl='" + TSUBGL + "',@FBrcd='" + FBRCD + "',@TBrcd='" + TBRCD + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
           
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

    public DataTable GetLoanOverdue_Only(string EDT, string BRCD, string DATE, string FSUBGL, string TSUBGL, string FL, string SFL)
    {
        try
        {
            if (FL == "OD")
            {
                sql = "EXEC ISP_OnlyLoanOverdue @Flag='OD',@SFlag='LOD',@SFlag1='" + SFL + "',@Brcd='" + BRCD + "',@FSbgl='" + FSUBGL + "',@TSbgl='" + TSUBGL + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetSurityRecord(string BRCD, string SGL, string AC, string EDT)
    {
        try
        {
            sql = "select LIMIT,CONVERT(VARCHAR(11),SANSSIONDATE,106) as Date1,INSTALLMENT,FLAT_ROOMNO as Address, Address1+','+Address2 as BranchAdd,BANKNAME,CONVERT(VARCHAR(11),GETDATE(),106) as Date  from LOANINFO I ";
            sql += "inner join ADDMAST A on A.CUSTNO=I.CUSTNO and A.BRCD=I.BRCD  ";
            sql += " inner join BANKNAME BN on BN.BRCD=A.BRCD ";
            sql += "where I.BRCD='" + BRCD + "' and I.CUSTACCNO='" + AC + "' and I.LOANGLCODE='" + SGL + "' and A.ADDTYPE=1 ";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return DT;
    }

    public DataTable GetSurityRecordNotice(string BRCD, string SGL, string AC, string EDT)//Amruta  27/09 for company notice address
    {
        try
        {
            sql = "select LIMIT,CONVERT(VARCHAR(11),SANSSIONDATE,106) as Date1,INSTALLMENT,FLAT_ROOMNO as Address, Address1+','+Address2 as BranchAdd,BANKNAME,CONVERT(VARCHAR(11),GETDATE(),106) as Date  from LOANINFO I ";
            sql += "inner join ADDMAST A on A.CUSTNO=I.CUSTNO and A.BRCD=I.BRCD  ";
            sql += " inner join BANKNAME BN on BN.BRCD=A.BRCD ";
            sql += "where I.BRCD='" + BRCD + "' and I.CUSTACCNO='" + AC + "' and I.LOANGLCODE='" + SGL + "' and A.ADDTYPE=3 ";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return DT;
    }

    public DataTable GetLoanNPAReport(string BrCd, string SGlCode, string AsOnDate, string Flag)
    {
        try
        {
            sql = "Exec RptLoanODNPAReport @Brcd='" + BrCd + "', @Sbgl='" + SGlCode + "', @OnDate='" + conn.ConvertDate(AsOnDate).ToString() + "', @Flag = '" + Flag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return DT;
    }
}