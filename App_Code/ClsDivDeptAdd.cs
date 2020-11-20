using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

/// <summary>
/// Summary description for ClsDivDeptAdd
/// </summary>
public class ClsDivDeptAdd
{
        DataTable Dt = new DataTable();
        DbConnection conn = new DbConnection();
    string sql = "", result="" ;
	public ClsDivDeptAdd()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string  GetDiv()
    {
        sql = "select IsNull( max(Recdiv)+1,1) Recdiv from paymast ";
        string result = conn.sExecuteScalar(sql);
        return result;
    }
      public string  GetDeptId(string divno)
    {
        sql = "select isnull(max(reccode)+1 ,0) as reccode from paymast where  recdiv='"+divno+"'";
        string result = conn.sExecuteScalar(sql);
        return result;
    }
    public int bindgrid(string LNO, GridView Gview)
    {
        sql = "select RECDIV,RECCODE,DESCR,REMARK,DELETELAG,TEMPID  from paymast  WHERE RECDIV='"+LNO+"' and stage=1003";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;
    }
    
     public int SaveData(string RECDIV,string RECCODE,string DESC,string REMARK,string DELETELAG,string TEMPID,string Brcd)
    {
        int result = 0;
        try
        {
            sql = "INSERT INTO PAYMAST(RECDIV,RECCODE,DESCR,REMARK,DELETELAG,TEMPID,STAGE,STATUS,BRCD,SYSTEMDATE) VALUES('" + RECDIV + "','" + RECCODE + "','" + DESC + "','" + REMARK + "','" + DELETELAG + "','" + TEMPID + "','1003','1','" + Brcd + "',GETDATE())";
            result = conn.sExecuteQuery(sql);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            
        }
        return result;
    }
     public int ModifyData(string RECDIV, string RECCODE, string DESC, string REMARK, string DELETELAG, string TEMPID)
     {
         int result = 0;
         try
         {
             sql = "update paymast set DESCR='"+DESC+"',REMARK='"+REMARK+"',DELETELAG='"+DELETELAG+"',TEMPID='"+TEMPID+"' where recdiv='"+RECDIV+"' and reccode='"+RECCODE+"'";
             result = conn.sExecuteQuery(sql);

         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);

         }
         return result;
     }
     public DataTable showdata(string recdiv, string reccode)
     {
         DataTable DT = new DataTable();
         sql = "select recdiv,reccode,DESCR,REMARK,DELETELAG,TEMPID from paymast where recdiv='"+recdiv+"' and reccode='"+reccode+"'";
         DT = conn.GetDatatable(sql);
         return DT;
     }
}