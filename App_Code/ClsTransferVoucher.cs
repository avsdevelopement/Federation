using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsTransferVoucher
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsTransferVoucher()
	{

	}

    public DataTable Transfer(string BRCD, string FDate, string MID)
    {

        try
        {
            string[] TD = FDate.Split('/');
            sql = "";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}