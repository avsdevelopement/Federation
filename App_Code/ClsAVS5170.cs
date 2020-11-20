using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

public class ClsAVS5170
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sResult = "", sql = "";

    public void BindBranch(DropDownList DDL, string BRCD)
    {
        DT = new DataTable();
        DS = new DataSet();
        try
        {
            sql = "Select ConVert(VarChar(10), BrCd)+'-'+ ConVert(VarChar(200), MidName) As Name, BrCd As ID " +
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
            DDL.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataSet ParameterReport(string BrCode)
    {
        DT = new DataTable();
        DS = new DataSet();
        try
        {
            sql = "Exec ISP_AVS0209 @BrCode = '" + BrCode + "'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
}