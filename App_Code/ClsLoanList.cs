using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsLoanList
/// </summary>
public class ClsLoanList
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsLoanList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable LoanList(string FBC, string GL, string FDate, string FD)  
    {
        try
        {
            sql = "Exec RptTopLoanList '" + FBC + "' ,'" + GL + "' ,'" + Conn.ConvertDate(FDate).ToString() + "' ,'" + FD + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}