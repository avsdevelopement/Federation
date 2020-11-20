using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for ClsFinanceYear
/// </summary>
public class ClsFinanceYear
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
    public ClsFinanceYear()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int GetFinaceYearTable(GridView GRD)
    {

        try
        {
            sql = "Exec RptBrWiseDepositLoanFisicalYr";
            Result = conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }
    public int GetDepLoanTable(GridView GRD, string FDATE, string TYPE)
    {
        try
        {
            sql = "Exec RptBrWiseDepositLoanList '" + conn.ConvertDate(FDATE) + "','" + TYPE + "'";
            Result = conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }
}