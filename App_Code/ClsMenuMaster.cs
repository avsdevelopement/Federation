using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsMenuMaster
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0, RM;

	public ClsMenuMaster()
	{
		
	}

    public int BindData(GridView Gview)
    {
        try
        {
            sql = "Select * From AVS5016";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string MenuId)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select * From AVS5016 Where MenuId = '" + MenuId + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public int InsertData(string ParentMenuId, string MenuTitle, string PageDesc, string PageUrl)
    {
        try
        {
            sql = "Exec Sp_MenuMaster @MenuId = '" + ParentMenuId + "', @MenuTitle = '" + MenuTitle + "', @PageDesc = '" + PageDesc + "', @PageURL = '" + PageUrl + "', @Flag = 'AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ModifyData(string MenuId, string ParentMenuId, string MenuTitle, string PageDesc, string PageUrl)
    {
        try
        {
            sql = "Exec Sp_MenuMaster @MenuId = '" + MenuId + "', @ParentMenuId = '" + ParentMenuId + "', @MenuTitle = '" + MenuTitle + "', @PageDesc = '" + PageDesc + "', @PageURL = '" + PageUrl + "', @Flag = 'MD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string MenuId)
    {
        try
        {
            sql = "Exec Sp_MenuMaster @MenuId = '" + MenuId + "', @Flag = 'DL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}