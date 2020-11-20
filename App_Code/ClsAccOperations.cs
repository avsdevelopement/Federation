using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsAccOperations
/// </summary>
public class ClsAccOperations
{
    DbConnection conn = new DbConnection();
    string sql = "", STATUS="",sql1="",sql2="",sql3="";
  int St=0;
  string Sts;
	public ClsAccOperations()
	{
		
	}
    public string SaveData(string FL, string BRCD, string PRD, string ACC, string GL)
    {
        try
        {
            if (FL == "ACSTATUS")
            {
                sql = "Exec SP_AccOperation @flag='" + FL + "',@BRCD='" + BRCD + "',@PRD='" + PRD + "',@ACC='" + ACC + "',@GLCODE='" + GL + "' ";
                Sts=conn.sExecuteScalar(sql);
                if (Sts == "1")
                {
                    STATUS = "OPEN";
                }
                else 
                {
                    STATUS = "CLOSED";
                }
            }
         }
        catch (Exception Ex)
        {
          ExceptionLogging.SendErrorToText(Ex);
        }
        return STATUS;
       
      }
    public int Updatedate(string FL, string BRCD, string PRD, string ACC, string GL, string DDL, string DATE)
    {
        try
        {
            if (FL == "UPDATE")
            {
                sql1 = "Exec SP_AccOperation @flag='" + FL + "',@BRCD='" + BRCD + "',@PRD='" + PRD + "',@ACC='" + ACC + "',@GLCODE='" + GL + "',@DDL='" + DDL + "',@EDate='" + conn.ConvertDate(DATE).ToString() + "' ";
                St = conn.sExecuteQuery(sql1);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return St;
        
    }

    public int CustData(string FL, string BRCD, string CNO,string DDL)
    {
        try
        {
            if (FL == "CUSTOMER")
            {
                sql2 = "Exec SP_AccOperation @flag='" + FL + "',@BRCD='" + BRCD + "',@CUST='" + CNO + "',@DDL='" + DDL + "'";
                St = conn.sExecuteQuery(sql2);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return St  ;
    }
    public string  GetBalance(string FL, string BRCD, string PRD, string ACC,string DATE)
    {
        try
        {
            if(FL=="OpBal")
            {
                sql3 = "Exec SP_OpClBalance @BrCode='" + BRCD + "',@SubGlCode='" + PRD + "',@AccNo='" + ACC + "',@EDate='" + conn.ConvertDate(DATE).ToString() + "',@Flag='" + FL + "'	";
                Sts = conn.sExecuteScalar(sql3);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Sts;
    }
}

