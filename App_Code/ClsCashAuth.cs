using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for ClsCashAuth
/// </summary>
public class ClsCashAuth
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();

    string sql = "";
    int Result;
	public ClsCashAuth()
	{
		
	}

    public int Getinfotable(GridView Gview, string smid, string sbrcd,string EDT)
    {
        try 
        {
        //string sql1 = " Select A.SETNO SETNO, ACC.SUBGLCODE AT, A.ACCNO ACNO, M.CUSTNAME CUSTNAME, A.CREDIT AMOUNT, A.PARTICULARS PARTICULARS, UM.USERNAME MAKER from ALLVCR A " +
        //      " LEFT JOIN USERMASTER UM ON UM.PERMISSIONNO=A.MID AND UM.BRCD=A.BRCD " +
        //      " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=A.ACCNO AND ACC.BRCD = A.BRCD AND A.SUBGLCODE=ACC.SUBGLCODE "+
        //      " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO  AND M.BRCD=ACC.BRCD"+
        //      " WHERE A.BRCD='"+sbrcd+"' AND A.STAGE = '1001' AND A.CREDIT<>'0' AND A.ACTIVITY='3' AND A.ENTRYDATE = '"+EDT.ToString()+"', 'dd/MM/yyyy')";
        
        sql = " Select A.SETNO SETNO, ACC.SUBGLCODE AT, A.ACCNO ACNO, M.CUSTNAME CUSTNAME, A.CREDIT AMOUNT, A.PARTICULARS PARTICULARS, UM.USERNAME MAKER from ALLVCR A  " +
             " INNER JOIN USERMASTER UM ON UM.PERMISSIONNO=A.MID AND UM.BRCD=A.BRCD   " +
             " INNER JOIN AVS_ACC ACC ON ACC.ACCNO=A.ACCNO AND ACC.BRCD = A.BRCD AND A.SUBGLCODE=ACC.SUBGLCODE  " +
             " INNER JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO  AND M.BRCD=ACC.BRCD  " +
             " WHERE A.BRCD='" + sbrcd + "' AND A.STAGE = '1001' AND A.CREDIT<>'0' AND A.ACTIVITY='3' " +
             " AND A.ENTRYDATE = '" + EDT.ToString() + "', 'DD-MM-YYYY') ";

        Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return Result;
    }

    // Cheking for maker
    public string Checkmaker(string setno, string brcd)
    {
        string rtn = "";
        try 
        {
        rtn = "";
        sql = "select MID from ALLVCR WHERE SETNO='" + setno + "' AND BRCD ='" + brcd + "' AND STAGE= '1001' AND CREDIT <>0 ";
        rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return rtn;
    }

}