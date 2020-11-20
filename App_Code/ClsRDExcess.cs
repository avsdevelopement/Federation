using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsRDExcess
/// </summary>
public class ClsRDExcess
{
    string sql = "";
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    string result = "";
    int rsltint = 0;
    double resultdouble = 0;
	public ClsRDExcess()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insertExcess(string brcd, string suglcode, string accno, string amount, string remarks,string MID)
    {
        sql = "UPDATE DEPOSITINFO SET PeriodDD='" + amount + "',REMARK='" + MID + remarks + "' WHERE BRCD='" + brcd + "' AND DEPOSITGLCODE='" + suglcode + "' AND CUSTACCNO='" + accno + "'";
        rsltint = conn.sExecuteQuery(sql);
        return rsltint;
    
    }
}