using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsDepositeGLCreation
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();

	public ClsDepositeGLCreation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}