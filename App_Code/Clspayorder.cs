using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Clspayorder
/// </summary>
public class Clspayorder
{

    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public Clspayorder()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetInfo(string chckno, string BRCD, string setno)
    {
        DataTable DT = new DataTable();
        try 
        {
        sql = "select * from ALLVCR where INSTRUMENTNO ='" + chckno + "' and brcd='" + BRCD + "' and setno='" + setno + "'";
      
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }

    public int insert (string txtpay,string brcd, string  txtset, string txtpayamt ,string  txtpaynam ,  string  txtcstno  ,  string txtaccno  ,  string txtnam , string txtissuenam)
    {
        try 
        {
        string sql = "insert into avs3000 (PAYORDERNO,SETNO,PAYORDERAMOUNT,IN_WORDS,CUSTNO,CUSTNAME,ISSUEDACCNO,refno) VALUES ('" + txtpay + "','" + txtset + "','" + txtpayamt + "','" + txtpaynam + "','" + txtcstno + "','" + txtnam + "','" + txtaccno + "','61')";
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    //public string Getrefno(string refno, string BRCD)
    //{
    //    sql = " SELECT MAX(REFNO)+1 FROM AVS3000 WHERE BRCD='" + BRCD + "'";
    //    refno = conn.sExecuteScalar(sql);
    //    return refno;
    //}


}