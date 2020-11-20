using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for ClsAVS5153
/// </summary>
public class ClsAVS5153
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "";

    public void BindBranch(DropDownList DDL, string BRCD)
    {
        DT = new DataTable();
        DS = new DataSet();
        try
        {
            sql = "Select ConVert(VarChar(10), BrCd)+'-'+ ConVert(VarChar(200), MIDNAME) As Name, BrCd As ID " +
                  "From BankName Where BrCd <> 0 Order By BrCd";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;

            DDL.DataSource = DS;
            DDL.DataTextField = "Name";
            DDL.DataValueField = "ID";
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("--ALL Branch--", "0"));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

  
}