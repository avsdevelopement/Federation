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
public class clsLookupMaster
{
    DbConnection conn = new DbConnection();
    string sql;
    DataTable DT;
    int result;
	public clsLookupMaster()
	{	
	}

   
     public int bindgrid(GridView Gview)
    {

        sql = "select DISTINCT LNO,LTYPE,DESCRIPTION from LOOKUPFORM1 order by LNO desc";
         int Result = conn.sBindGrid(Gview, sql);
        return Result;


    }
    public DataTable BindGrid1()
    {
        try
        {
            sql = "SELECT DISTINCT LNO,LTYPE FROM LOOKUPFORM";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            return DT;
        }
        catch (Exception Ex)
        {
            ////WebMsgBox.Show(Ex.Message);
            return DT = null;
        }
    }
    
    
    public int SaveData(string  LNO,string LTYPE,string SRNO,string DESC,string DESCMAR,string REFNO)
    {
        try
        {
            sql = "insert into LOOKUPFORM1 (LNO,LTYPE,DESCRIPTION,SRNO,DESCRIPTIONMAR,REFNO) VALUES ('"+LNO+"','"+LTYPE+"','"+DESC+"','"+SRNO+"','"+DESCMAR+"','"+REFNO+"')";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }
    public int savelookup(int lno, string ltype, string desc)
    {
        try
        {
            sql = "insert into LOOKUPFORM1(LNO,LTYPE,DESCRIPTION)values('"+lno+"','"+ltype+"','"+desc+"')";
            result = conn.sExecuteQuery(sql);
            return result;

        }
        catch (Exception Ex)
        {
            return result = 0;
        }
    }
    public int ModifyData(string  LNO,string LTYPE,string SRNO,string DESC,string DESCMAR,string REFNO)
    {
        try
        {
            sql = "UPDATE LOOKUPFORM1 SET LTYPE='"+LTYPE+"',DESCRIPTION='"+DESC+"',DESCRIPTIONMAR='"+DESCMAR+"',REFNO='"+REFNO+"' WHERE LNO='"+LNO+"' AND SRNO='"+SRNO+"'";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }
    public int Modifysublook(int LNO, string LDESC,string ltype)
    {
        try
        {
            sql = "UPDATE LOOKUPFORM1 SET DESCRIPTION='" + LDESC + "' WHERE LNO='" + LNO + "' and LTYPE='" + ltype + "'";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ////WebMsgBox.Show(Ex.Message);
            return result = 0;
        }
    }
    public int modifylookup(int lno,string desc)
    {
        try
        {
            sql = "update LOOKUPFORM1 set DESCRIPTION='"+desc+"' where LNO='"+lno+"'";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            return result = 0;
        }
    }
    public int DeleteData(int LNO)
    {
        try
        {
          
           sql = "DELETE FROM LOOKUPFORM1 WHERE LNO='"+LNO+"'";
            result = conn.sExecuteQuery(sql);
            return result;
        }
        catch (Exception Ex)
        {
            ////WebMsgBox.Show(Ex.Message);
            return result = 0;
        }
    }
    public int deletelookup(string  LNO,string srno)
    {
        try
        {
            sql = "delete from LOOKUPFORM1 where LNO='"+LNO+"' AND SrNo='"+srno+"'";
            result = conn.sExecuteQuery(sql);
            return result;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result = 0;
        }
    }

   
     public string getdata(string LNO)
    {
        
            sql = "select LTYPE from LOOKUPFORM where LNO='"+LNO+"' group by LNO ,LTYPE";
            string Result = conn.sExecuteScalar(sql);

            return Result;
    }
     public DataTable show1(string lno,string srno)
     {
         sql = "select LNO,	LTYPE,	DESCRIPTION,SRNO,DESCRIPTIONMAR,REFNO from LOOKUPFORM1 where LNO='"+lno+"' AND SrNo='"+srno+"'";
         DT = conn.GetDatatable(sql);
         return DT;
     }
     public DataTable showsub(string lno)
     {
         sql = "select LNO,LTYPE,DESCRIPTION from LOOKUPFORM1 where LNO='"+lno+"' ";
         DT = conn.GetDatatable(sql);
         return DT;
     }
     public string  autoid()
     {
         sql = "select MAX(LNO)+1 from LOOKUPFORM1";
         result =Convert.ToInt32( conn.sExecuteScalar(sql));
         return result.ToString();

     }
     public int bindgrid(string LNO, GridView Gview)
     {
         sql = "SELECT * FROM  LOOKUPFORM1 where lno='"+LNO+"' order by SRNO desc";
         int Result = conn.sBindGrid(Gview, sql);
         return Result;
     }
     public string getdetails(string LNO)
     {
         sql = "SELECT COUNT(*)+1 FROM LOOKUPFORM1 WHERE LNO='"+LNO+"'";
         string Result = conn.sExecuteScalar(sql);
         return Result;

     }
     public string GETLTYPE(string LNO)
     {
         sql = "select DISTINCT LTYPE from lookupform1  WHERE LNO='"+LNO+"'";
         string RESULT = conn.sExecuteScalar(sql);
         return RESULT;
     }
    public string GETSRNO(string LNO)
    {
        sql = "select MAX(SRNO)+1 from lookupform1  WHERE LNO='"+LNO+"' ";
        string RESULT = conn.sExecuteScalar(sql);
        return RESULT;
    }
}