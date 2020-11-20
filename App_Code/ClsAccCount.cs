using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsAccCount
/// </summary>
public class ClsAccCount
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult="";
    int Result = 0;

	public ClsAccCount()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetCount(string BRCD, string GLCODE)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "EXEC SP_COUNTACCNO @BRCD='" + BRCD + "',@GLCODE='" + GLCODE + "'";
            dt = new DataTable();
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
   
    
  
    public string GetAccStatus(string Brcd, string Subglcode, string AccNo)//Dhanya Shetty//06/02/2018
    {
        try
        {
            sql = "select ACC_STATUS from avs_acc where BRCD='" + Brcd + "' and Subglcode='" + Subglcode + "' and Accno='"+AccNo+"'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public int CheckAccSameDetails(string BRCD, string SUBGLCODE, string ACCNO, string ACC_STATUS, string EFFECTDATE)//Dhanya Shetty//13/02/2018
    {
        try
        {
            sql = "select count(*) from avs5032 where BRCD='" + BRCD + "' and SUBGLCODE='" + SUBGLCODE + "' and ACCNO='" + ACCNO + "' and  ACC_STATUS='" + ACC_STATUS + "' and EFFECTDATE='" + conn.ConvertDate(EFFECTDATE) + "'";
           Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
  
}