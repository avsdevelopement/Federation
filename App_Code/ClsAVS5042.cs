using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsAVS5042
{
    DataTable DT=new DataTable();
    string sql = "",Amt="";
    DbConnection Conn = new DbConnection();

	public ClsAVS5042()
	{
	
	}
 
    public string GetSanctionAmount(string BRCD, string LOANGL, string ACCNO)
    {
        try
        {
            sql = "select limit from  loaninfo  where brcd='" + BRCD + "' and LOANGLCODE='" + LOANGL + "' and CUSTACCNO='" + ACCNO + "'  and lmstatus=1";
            Amt = Conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Amt;
    }
}
 