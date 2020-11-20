using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.WebControls;


public class ClsWchangePWD
{
    DbConnection conn = new DbConnection();

    public ClsWchangePWD()
    {
        
    }

    public int SubmitWchangepwd(string txtnewpwd, string txtlogincode, string txtoldpwd, string BRCD,string mid)//BRCD ADDED --Abhishek
    {
        DataTable DT = new DataTable();

        string sql = "Select LOGINCODE, EPASSWORD  from USERMASTER where LOGINCODE = '" + txtlogincode + "' AND EPASSWORD = '" + txtoldpwd + "' and BRCD='" + BRCD + "'";
        DT = conn.GetDatatable(sql);

        if (DT.Rows.Count > 0)
        {
            string sql1 = "update USERMASTER SET STAGE ='1002' , EPASSWORD ='" + txtnewpwd + "' where LOGINCODE ='" + txtlogincode + "' and BRCD='" + BRCD + "' and PERMISSIONNO='"+mid+"'";
            conn.sExecuteQuery(sql1);
            return 1;
        }
        else
        {
            return 0;
        }
        
  
    }
    public string getpermissionno(string logincode, string epasswd, string BRCD)
    {
        string res = "";
        try
        {
           string sql="select permissionno from usermaster where  LOGINCODE = '" + logincode + "' AND EPASSWORD = '" + epasswd + "' and BRCD='" + BRCD + "'";
           res = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public void binddll(DropDownList ddl, string brcd)
    {
        string res = "";
        try
        {
            string sql = "select username as name,permissionno as id from usermaster where BRCD='" + brcd + "'";
            conn.FillDDL(ddl, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
      
    }
    public string getlogincd(string mid)
    {
        string res = "";
        try
        {
            string sql = "select logincode from usermaster where PERMISSIONNO='" + mid + "'";
            res = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
}