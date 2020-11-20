using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ClsSetPWD
{
    int CN;

	public ClsSetPWD()
	{
		
	}

    public int submitAsspwd(string txtEnterpwd, string loginCD,string BRCD) //BRCD ADDED --Abhishek
    {
        try
        {
            DbConnection conn = new DbConnection();
            string sql = "update USERMASTER set STAGE ='1002', PASSWORD ='" + txtEnterpwd + "' where logincode='" + loginCD + "' and BRCD='" + BRCD + "'";
            CN = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return CN = 0;
        }
        return CN;
    } 
}