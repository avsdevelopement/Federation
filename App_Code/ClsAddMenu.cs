using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
/// <summary>
/// Summary description for ClsAddMenu
/// </summary>
public class ClsAddMenu
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sQuery = "", sCustNo = "", sMobNo = "";
    string sql = "", sResult = "";
    int Result = 0;
    double balance = 0;
    public ClsAddMenu()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int AddMenuTitle(string MenuId, string ParentMenuId, string MenuTitle, string PageDesc, string PageURL, string STATUS)
    {
        int RM;
       
        try
        {
            string id;
            if (STATUS =="1")
            {
                id="1";
            }else{
                id="0";
            }
            sql = "Insert Into avs5016 (MenuId, ParentMenuId, MenuTitle, PageDesc, PageURL, STATUS, USERGR1, USERGR2, USERGR3, USERGR4, USERGR5) "
                + " VALUES ('" + MenuId + "',Case when '" + ParentMenuId + "'='' then 0 else '" + ParentMenuId + "' end ,'" + MenuTitle + "','" + PageDesc + "','" + PageURL + "','" + STATUS + "','" + id + "','" + id + "','" + id + "','" + id + "','" + id + "' )";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public string ChkUsrExists(string MenuId)
    {
        try
        {
            sql = "SELECT MenuId FROM avs5016 WHERE  MenuId = '" + MenuId + "' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string GetParent(string MenuId)
    {
        try
        {
            sql = "select  dbo.MenuName('" + MenuId + "') ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string ChkMenuTitle( string MenuTitle,string ParentMenuID)
    {
        try
        {
           // sql = "SELECT ParentMenuId,MenuTitle FROM avs5016 WHERE  ParentMenuId = '" + MenuTitle + "' and  MenuTitle='" + MenuId + "' ";

            sql = "SELECT ParentMenuId,MenuTitle FROM avs5016 WHERE  MenuTitle = '" + MenuTitle + "' and ParentMenuID='" + ParentMenuID + "'   ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable MenuTitleDes(string MenuId)
    {
        try
        {
            sql = "SELECT MenuId, ParentMenuId, MenuTitle, PageDesc, PageURL, STATUS FROM avs5016 WHERE  MenuId = '" + MenuId + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
       
    }



    public int UpdateMenuTitle(string MenuId, string ParentMenuId, string MenuTitle, string PageDesc, string PageURL, string STATUS)
    {
        int RM;
        try
        {
            string id;
            if (STATUS == "1")
            {
                id = "1";
            }
            else
            {
                id = "0";
            }

            sql = " update avs5016 set MenuTitle = '" + MenuTitle + "' ,PageDesc='" + PageDesc + "', PageURL='" + PageURL + "', ParentMenuId='" + ParentMenuId + "',STATUS='" + STATUS + "',USERGR1='" + id + "',USERGR2='" + id + "',USERGR3='" + id + "',USERGR4='" + id + "',USERGR5='" + id + "' where MenuId='" + MenuId + "' ";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public DataTable rptMenu()
    {
        try
        {
            sql = "Exec SP_Menu";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;

    }

}