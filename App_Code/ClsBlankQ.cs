using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsBlankQ
/// </summary>
public class ClsBlankQ
{
    DbConnection conn = new DbConnection();
    string Sql,Result;
    int Rtn;
	public ClsBlankQ()
	{
		
	}
    public int InsertData(string LoginId, string Name, string MobileNo, string ModuleRQ, string QueryDes, string BRCD, string BankCode, string priority, string MID)
    {
        try
        {
            Sql = "Insert into AVSCHD2(LoginId,Name,MobileNo,ModueRQ,QueryDes,BRCD,BankCode,TakenBy,status,priority,STAGE,MID) values('" + LoginId + "','" + Name + "','" + MobileNo + "','" + ModuleRQ + "','" + QueryDes + "','" + BRCD + "','" + BankCode + "','','1','"+priority+"','1001','"+MID+"') ";
            Rtn = conn.sExecuteQuery1(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Rtn;
    }
    public string GetMobile(string LoginId)
    {
        try
        {
            Sql = "select usermobileno from USERMASTER WHERE LOGINCODE='" + LoginId + "'";
            Result = conn.sExecuteScalar(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetIssueNo()
    {
        try
        {
            Sql = "select isnull(MAX(IssueNo),0)+1 from AVSCHD2 ";
         Result = conn.sExecuteScalar1(Sql);
          }
        catch (Exception Ex)
        {
           ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetBnkCde(string bnkname)
    {
        try
        {
            Sql = "select bankcd from bankname where midname='"+bnkname+"'";
            Result = conn.sExecuteScalar(Sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}