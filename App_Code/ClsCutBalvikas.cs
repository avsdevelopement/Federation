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

/// <summary>
/// Summary description for ClsCutBalvikas
/// </summary>
public class ClsCutBalvikas
{
    
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();
	public ClsCutBalvikas()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string  getname(string BRCD,string SUBGLCODE)
    {
       sql="SELECT AGENTNAME FROM AGENTMAST WHERE BRCD='"+BRCD+"' AND AGENTCODE='"+SUBGLCODE+"'";
     string  Result=conn.sExecuteScalar(sql);
        return Result;
    }
    public string glcode(string brcd, string sublgcode)
    {
        sql = "select glcode from glmast where brcd='"+brcd+"' and subglcode='"+sublgcode+"'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
}