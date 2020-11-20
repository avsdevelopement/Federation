using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ClsWAssPWD
{
    DbConnection conn = new DbConnection();
    string sql;

	public ClsWAssPWD()
	{
		
	}

    public int AssignPass(string txtEnterpwd, string loginCD, string BRCD)//BRCD ADDED --Abhishek
    {
        int RM;
        try
        {
            sql = "UPDATE USERMASTER SET STAGE ='1002', EPASSWORD ='" + txtEnterpwd + "' WHERE logincode='" + loginCD + "' and BRCD='" + BRCD + "'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }
}