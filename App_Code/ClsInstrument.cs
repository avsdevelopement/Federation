using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for ClsInstrument
/// </summary>
public class ClsInstrument
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
   
	public ClsInstrument()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Getacctype(string acctype, string BRCD)
    {
        try 
        {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE ='" + acctype + "' AND BRCD='" + BRCD + "' and glcode=1";
        acctype = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return acctype;
    }

    public int insert(string acctype, string BRCD,string accno,string noleaves,string fromno,string tono , string issuedate)
    {
        try 
        {
        string sql = "insert into avs4000 (subglcode,brcd,accno,leave,intfrom,intto,issuedate,stage,glcode) " +
             " values ('" + acctype + "','" + BRCD + "','" + accno + "','" + noleaves + "','" + fromno + "', '" + tono + "', '" + issuedate + "','1001','1')";
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public int BindADD(GridView Gview,string BRCD)
    {
        try 
        {
            string sql = "select * from avs4000 where stage<>1004 and BRCD='" + BRCD + "'";//BRCD ADDED --Abhishek
        Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public int delete(string accno,string BRCD)
    {
        try 
        {
            sql = "update avs4000 set STAGE='1004' where stage<>1003 AND AID='" + accno + "' and BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;

    }

    public int authorize(string ACCNO,string BRCD)
    {
        try 
        {
            sql = "update avs4000 set STAGE='1003' where ACCNO='" + ACCNO + "'AND STAGE<>1004 and BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;

    }

    public DataTable GetInfo( string BRCD, string accno)
    {
        DataTable DT = new DataTable();
        try 
        {
        sql = "select * from avs4000 where  brcd='" + BRCD + "' and accno='" + accno + "'";
       
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }

    

     //public int modify(string acctype, string BRCD, string accno, string noleaves, string fromno, string tono)
     //{
     //    string sql = "update avs4000 set subglcode='" + acctype + "',brcd='" + BRCD + "',accno='" + accno + "',leave='" + noleaves + "',intfrom='" + fromno + "',intto='" + tono + "',stage='1002',glcode='1'"; 
     //    Result = conn.sExecuteQuery(sql);
     //    return Result;
     //}

     public string getname(string BRCD, string accno, string txttyp)
     {
         try 
         {
             sql = "select m.custname from AVS_ACC ac inner join master m on m.custno=ac.custno and m.brcd=ac.brcd where ac.subglcode='" + txttyp + "' and ac.accno='" + accno + "' and ac.BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
         BRCD = conn.sExecuteScalar(sql);
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
             //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
         }   
         return BRCD;
     }


     public string GetStage(string BRCD, string accno)
     {
         try 
         {
         sql = "SELECT STAGE FROM avs4000 WHERE BRCD='" + BRCD + "' AND accno='" + accno + "'";
         BRCD = conn.sExecuteScalar(sql);
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
             //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
         }   
         return BRCD;
     }
}