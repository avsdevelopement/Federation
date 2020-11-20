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
/// Summary description for ClsHead2Head
/// </summary>
public class ClsHead2Head
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    DataTable DT = new DataTable();
	public ClsHead2Head()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string getglname(string glcode,string BRCD)
    {
        sql = "select glname from glmast where glcode='" + glcode + "' and brcd='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string GETSUBGL(string GLCODE, string SUBGL, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE GLCODE='" + GLCODE + "' AND SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public int HEADTOHEAD(string FYEAR, string TYEAR, string FMONTH, string TMONTH, string BRCD, string NEWGL, string NEWSUB, string OLDGL, string OLDSUB)
    {
        sql = "EXEC HeadToHead_SP @FROMYEAR='"+FYEAR+"',@TOYEAR='"+TYEAR+"',@FROMMONTH='"+FMONTH+"',@TOMONTH='"+TMONTH+"',@BRCD='"+BRCD+"',@Newglcode='"+NEWGL+"',@NewSubglcode='"+NEWSUB+"',@Oldglcode='"+OLDGL+"',@OldSubglcode='"+OLDSUB+"'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
}