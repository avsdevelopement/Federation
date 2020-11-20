using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5090
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "",Srno="";
    int Result;
	public ClsAVS5090()
	{
		
	}

    public int BindRequ(GridView Gview, string Date)
    {
        try
        {
            sql = "select id,Srno,Convert(varchar(11),Date,103) as Date,Menu,Activity,Requirement,RequirementBy " +
                " from AVS5066 where    stage<>1004 order by  Date Desc  ";
            //Date='" + conn.ConvertDate(Date).ToString() + "' and
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetReqDate(string id)
    {
        try
        {
            sql = "select id,Srno,Convert(varchar(11),Date,103) as Date,Menu,Activity,Requirement,RequirementBy,Image_Name,ImageCode" +
                " from AVS5066 WHERE   Id = '" + id + "' and stage<>1004";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }
    public string GetSrno()
    {
        try
        {
            sql = "select ISNULL(MAX(Srno), 0) + 1 As Srno  from AVS5066 where stage<>1004";
            Srno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Srno;
    }
    public DataTable GetDetails(string Edate)
    {
        try
        {
            //Date='" + conn.ConvertDate(Date).ToString() + "' and 
            sql = "Select id, Srno,Convert(varchar(11),Date,103)Date,Menu,Activity,Requirement,RequirementBy,Image_Name,ImageCode from AVS5066 where  stage<>1004 order by Date Desc";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}