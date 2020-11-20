using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsGetBrDPLNList
/// </summary>
public class ClsGetBrDPLNList
{
    DataSet DS = new DataSet();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetBrDPLNList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetBrDPLNData(string fdate, string FL)
    {
        try
        {
            sql = "Exec RptBrWiseDepositLoanList @AsOnDate='" + Conn.ConvertDate(fdate) + "',@Type='" + FL + "'";
            DS = new DataSet();
            DS.Tables.Add(Conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
}