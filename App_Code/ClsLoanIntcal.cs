using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanIntcal
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsEncryptValue EV = new ClsEncryptValue();
    string EMD = "", sql = "";
    int Result = 0;

    public ClsLoanIntcal()
    {

    }

    public string GetINTBK(string BRCD)
    {
        try
        {
            sql = "SELECT LISTVALUE FROM PARAMETER WHERE BRCD='" + BRCD + "' AND LISTFIELD='INTBK'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetAvailData(string EDT, string FBRCD, string TBRCD, string MID)
    {
        try
        {
            sql = "EXEC SP_LOANINT_TRASFER @FLAG='IFDATA',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@EDT='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public int BindGrid(GridView Gview, string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string ASDT, string EDT, string MID)
    {
        try
        {
            sql = "EXEC SP_LOANINT_TRASFER @FLAG='TRAILSHOW',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRDCD='" + FPRD + "',@TPRDCD='" + TPRD + "',@FACCNO='" + FACC + "',@TACCNO='" + TACC + "',@EDT='" + conn.ConvertDate(ASDT) + "',@OnEDT='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

  

   

    public DataTable GetReport(string FBRCD, string TBRCD, string FPRD, string TPRD, string FACC, string TACC, string POSTDATE, string EDT, string MID)
    {
        DT = new DataTable();
        try
        {
            sql = "EXEC SP_LOANINT_TRASFER @FLAG='TRAILSHOW',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRDCD='" + FPRD + "',@TPRDCD='" + TPRD + "',@FACCNO='" + FACC + "',@TACCNO='" + TACC + "',@EDT='" + conn.ConvertDate(POSTDATE) + "',@OnEDT='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    
}