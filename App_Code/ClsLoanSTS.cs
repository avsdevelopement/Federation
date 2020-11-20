using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ClsLoanSTS
{
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    public ClsLoanSTS()
    {
    }
    public int BindGrid(GridView Gview, string Fdate, string TDate, string BRCD, string GLCD, string SGLCD, string ACC,string GL)
    {
        try
        {
            string FL = "";
            if (GL == "3")
            {
                FL = "LOANSTS";
            }
            else if (GL == "5")
            {
                FL = "DEPOSTS";
            }
            sql = "EXEC SP_LOANACCSTS @FLAG ='"+FL+"',@FDATE='" + conn.ConvertDate(Fdate) + "',@TDATE='" + conn.ConvertDate(TDate) + "',@PRDTCD ='" + SGLCD + "',@GLCD='" + GLCD + "',@ACCNO ='" + ACC + "',@BRCD='" + BRCD + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetCustInfo(string BRCD, string ACCNO, string PRDCD)
    {
        try
        {
            sql = "Exec SP_LOANACCSTS @FLAG ='GETINFO',@accno='" + ACCNO + "',@PRDTCD='" + PRDCD + "',@BRCD='" + BRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);            
        }
        return DT;
    }
    public DataTable GetDepositInfo(string BRCD, string ACCNO, string PRDCD)
    {
        try
        {
            sql = "Exec SP_LOANACCSTS @FLAG ='GETDEPOSIT',@accno='" + ACCNO + "',@PRDTCD='" + PRDCD + "',@BRCD='" + BRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}