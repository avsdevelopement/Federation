using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

public class ClsCalculator
{
    string sql = "";
    string rtn = "";
    int rtnint = 0;
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();

	public ClsCalculator()
	{

	}

    public float GetIntrestRate(string procode, string period, string brcd, string periodtype, bool TF)
    {
        float rtnf = 0;
        try
        {
            if (TF == true)
            {
                sql = "SELECT RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "') AND BRCD ='" + brcd + "' AND PERIODTYPE='D'";
            }
            else
            {
                sql = "SELECT RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "') AND BRCD ='" + brcd + "' AND PERIODTYPE='" + periodtype + "'";
            }
            rtn = conn.sExecuteScalar(sql);
            if (rtnf != null)
            {
                rtnf = float.Parse(rtn);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtnf;
    }

    public string CheckPeriod(string ACTYPE,string procode, string period, string periodtype, string brcd)
    {
        string IsPValid = "0";
        try
        {
            sql = "select RATE FROM A50001 WHERE TDCUSTTYPE = '" + ACTYPE + "' AND PERIODTYPE = '" + periodtype + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >= '" + period + "') AND DEPOSITGL ='" + procode + "' and BRCD='"+ brcd +"'"; //BRCD ADDED --Abhishek
            IsPValid = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return IsPValid;
    }
}