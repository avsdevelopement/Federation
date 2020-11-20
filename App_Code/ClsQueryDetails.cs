using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsQueryDetails
/// </summary>
public class ClsQueryDetails
{
    string sql = "";
    int res = 0;
    DbConnection conn = new DbConnection();
    public ClsQueryDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int GetPending(GridView grd, string loginid, string brcd, string bnkcde)
    {
        try
        {
            sql = "select ModueRQ,QueryDes,convert(varchar(10),ISSUEDATE,103)ISSUEDATE from avschd2 where LoginId='" + loginid + "' and BRCD='" + brcd + "' AND BANKCODE='" + bnkcde + "' and status in (1,2)";
            res = conn.sBindGrid1(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int GetSolevd(GridView grd, string brcd, string bnkcde)
    {
        try
        {
            sql = "select A.srno,A.MODULERQ,A.querydesc,convert(varchar(10),B.ISSUEDATE,103) ISSUEDATE,convert(varchar(10),A.currenttime,103) currenttime,convert(varchar(10),A.systemdate,103) systemdate from ASSIGNDETAILS A inner join AVSCHD2 B on A.ISSUENO=B.ISSUENO where A.STATUS=3 and B.BRCD='" + brcd + "' AND B.BANKCODE='" + bnkcde + "'";
            res = conn.sBindGrid1(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public string GETPARAM()
    {
        try
        {
            sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='SENDQUERY'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return sql;
    }


    // Added by Abhishek
    public int CreateCutBook_Pal(GridView Gd, string MID, string GLCD, string SUBGLCD, string FBRCD,string TBRCD, string EDT)
    {
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        double BALANCE = 0;
        try
        {
            string[] TD = EDT.Split('/');

            try
            {
                sql = "Exec ISP_AVS0166 @AsonDate='" + conn.ConvertDate(EDT) + "',@FBrCd='" + FBRCD + "' ,@TBrCd='" + TBRCD + "' ,@GlCode='" + GLCD + "',@SubGlCode='" + SUBGLCD + "',@MID='" + MID + "' ";
                res = conn.sBindGrid(Gd, sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return res;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return res;
    }
    public int CreateCutBook(GridView Gd, string MID, string GLCD, string SUBGLCD, string FBRCD, string TBRCD, string EDT)
    {
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        double BALANCE = 0;
        try
        {
            string[] TD = EDT.Split('/');

            try
            {
                sql = "Exec ISP_AVS0167 @AsonDate='" + conn.ConvertDate(EDT) + "',@FBrCd='" + FBRCD + "' ,@TBrCd='" + TBRCD + "' ,@GlCode='" + GLCD + "',@SubGlCode='" + SUBGLCD + "',@MID='" + MID + "' ";
                res = conn.sBindGrid(Gd, sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return res;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return res;
    }
    public string GetSanssionDate(string Brcd, string Subgl, string accno)
    {
        try
        {
            sql = "Select top 1 Convert(varchar(10),SANSSIONDATE,103) from LOANINFO A " +
                " where brcd='" + Brcd + "' " +
                " and LOANGLCODE='" + Subgl + "' " +
                " and CUSTACCNO='" + accno + "' " +
                " and LMSTATUS=(Select Min (LMSTATUS) from LOANINFO B " +
                " where B.BRCD=A.BRCD " +
                " and B.LOANGLCODE=A.LOANGLCODE " +
                " and B.CUSTACCNO=A.CUSTACCNO)"; 
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public int LoanBalanceReport(GridView GD,string FBrCode, string TBrCode, string FPrCode, string TPrCode, string AsOnDate, string Flag)
    {
        try
        {
            sql = "Exec RptLoanBalanceReport @FBrCode = '" + FBrCode + "', @TBrCode = '" + TBrCode + "', @FPrCode = '" + FPrCode + "', @TPrCode = '" + TPrCode + "', @AsOnDate = '" + conn.ConvertDate(AsOnDate) + "', @Flag = '" + Flag + "'";
            res = conn.sBindGrid(GD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
    public int LoanBalanceDash(GridView GD, string FBrCode, string TBrCode, string FPrCode, string TPrCode, string AsOnDate, string Flag)
    {
        try
        {
            sql = "Exec RptAllLnBalListOD_DASH @FBrCode = '" + FBrCode + "', @TBrCode = '" + TBrCode + "', @FPrCode = '" + FPrCode + "', @TPrCode = '" + TPrCode + "', @AsOnDate = '" + conn.ConvertDate(AsOnDate) + "', @Flag = '" + Flag + "'";
            res = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
}