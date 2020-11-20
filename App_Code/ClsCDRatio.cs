using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsCDRatio
/// </summary>
public class ClsCDRatio
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    DataTable DT = new DataTable();
	public ClsCDRatio()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void Bindcdtype(DropDownList ddlpart)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id from LOOKUPFORM1 WHERE LNO=1086 ORDER BY SRNO";
        conn.FillDDL(ddlpart, sql);
    }
    public void BindRPTtype(DropDownList ddlpart)
    {
        sql = "SELECT DESCRIPTION Name,SRNO id from LOOKUPFORM1 WHERE LNO=1087 ORDER BY SRNO";
        conn.FillDDL(ddlpart, sql);
    }
       public void BindGlGroup(DropDownList ddlpart)
    {
        sql = "SELECT GLGROUP Name ,SRNO id FROM BSFORMAT";
        conn.FillDDL(ddlpart, sql);
    }
    public string Getaccno(string AT, string BRCD)
       {
           string Result = "";
           try
           {
               sql = "SELECT (CONVERT (VARCHAR(10),SUBGLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY SUBGLCODE,GLNAME";
               Result = conn.sExecuteScalar(sql);
           }
           catch (Exception Ex)
           {
               ExceptionLogging.SendErrorToText(Ex);
           }
           return Result;
       }
    public int insertdata(string brcd, string pcode, string pname, string cdtype, string glgroup, string rpttype, string glpriority)
    {
        sql = "insert into avs5020(brcd,cdtype,glcode,subglcode,glname,glgroup,glpriority,rpttype) values('" + brcd + "','" + cdtype + "','" + pcode + "','" + pcode + "','" + pname + "','" + glgroup + "','" + glpriority + "','" + rpttype + "')";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int Modifydata(string brcd, string pcode, string pname, string cdtype, string glgroup, string rpttype, string glpriority)
    {
        sql = "update avs5020 set cdtype='" + cdtype + "',glgroup='" + glgroup + "',glpriority='" + glpriority + "',rpttype='" + rpttype + "' where brcd='" + brcd + "' and subglcode='"+pcode+"'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int deleteCDR(string brcd, string pcode)
    {
        sql = "delete from avs5020 where  brcd='" + brcd + "' and subglcode='" + pcode + "'";
        int Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string glname(string brcd, string pcode)
    {
        sql = "select glname from glmast where brcd='"+brcd+"' and subglcode='"+pcode+"'";
        String Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string getbrname(string brcd)
    {
        sql = "select midname from bankname  where brcd='"+brcd+"'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public DataTable getCDratio(string brcd, string asondate, string flag)
    {
        try
        {
            string[] FDT = asondate.Split('/');
            sql = "EXEC SP_CDRATIO  '" + FDT[1].ToString() + "','" + FDT[2].ToString() +"','"+conn.ConvertDate(asondate)+"','"+brcd+"','"+flag+"' ";
            DT = conn.GetDatatable(sql);
           
        }
        catch (Exception Ex)
        { 
        ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int bindgrid(GridView grd,string pcode,string brcd)
    {
    sql="select CDTYPE,SUBGLCODE,GLNAME,GLPRIORITY, RPTTYPE from avs5020 where subglcode='"+pcode+"' and brcd='"+brcd+"'";
      int Rs=  conn.sBindGrid(grd,sql);
      return Rs;
    }
    public DataTable showdata(string cdtype, string subglcode, string rpttype)
    {
        DataTable DT = new DataTable();
        sql = "select CDTYPE,SUBGLCODE,GLNAME,GLGROUP,GLPRIORITY,RPTTYPE from avs5020 where cdtype='"+cdtype+"' AND RPTTYPE='"+rpttype+"' AND SUBGLCODE='"+subglcode+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
}