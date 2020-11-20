using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

/// <summary>
/// Summary description for ClsWforgotPWD
/// </summary>
public class ClsWforgotPWD
{
    DbConnection conn = new DbConnection();

	public ClsWforgotPWD()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Submitforgetpwd(string txtloginId, string txtcreatenewpwd, string txtconfirmpwd,string BRCD)//BRCD ADDED --Abhishek
    {

        DataTable DT = new DataTable();
        string sql = "select LOGINCODE FROM USERMASTER WHERE LOGINCODE = '" + txtloginId + "' and BRCD='" + BRCD + "'";
        DT=conn.GetDatatable(sql);

        if (DT.Rows.Count > 0)
        {
            string sql1 = "update USERMASTER set STAGE ='1002', PASSWORD ='" + txtcreatenewpwd + "' WHERE LOGINCODE = '" + txtloginId + "' and BRCD='" + BRCD + "'";
            conn.sExecuteQuery(sql1);
            return 1;
        }
        else
        {

            return 0;
        }

    }

}