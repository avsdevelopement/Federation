using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for clsSanchitCertificate
/// </summary>
public class clsSanchitCertificate
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    DataTable DT = new DataTable();
	public clsSanchitCertificate()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string getBRCDname(string brcd)
    {
        sql = "select midname from bankname where brcd='" + brcd + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }

    public string Acname(string brcd, string accno)
    {
        sql = "select m.custname from DEPOSITINFO s LEFT join master m on s.custno=m.custno where S.CUSTACCNO='" + accno + "'" + "AND S.Depositglcode = '311'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }

    public DataTable GetSanchitCerti_TZMP(string BRCD, string AccNo, string Entrydate, string MID) // Added by Akshay 16-08-18
    {
        sql = "EXEC ISP_AVS0181 '" + BRCD + "','" + AccNo + "','" + conn.ConvertDate(Entrydate) + "','" + MID + "','SanchitTZMP'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
}