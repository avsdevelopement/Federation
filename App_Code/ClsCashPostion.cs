using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; 

/// <summary>
/// Summary description for ClsCashPostion 
/// </summary>
public class ClsCashPostion
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";
    public ClsCashPostion()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable CashPostionList(string BRCD,string Ason)
    {
        try
        {
            Dt = Conn.GetDatatable("Exec RptCashPostionReport '" + BRCD + "' , '" + Ason + "' ");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable ChairmanDT(string BranchID, string Ason)
    {
        try
        {
            sql = "Exec RptChairmanReport '" + BranchID + "' , '" + Ason + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}