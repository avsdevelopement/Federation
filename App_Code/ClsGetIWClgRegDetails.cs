using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsGetIWClgRegDetails
/// </summary>
public class ClsGetIWClgRegDetails
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";

	public ClsGetIWClgRegDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetIWReg(string BranchID, string FBKcode, string FDT, string SGL, string FL, string FLT)
    {
        try
        {
            if (FL == "IW")
            {
                if (FLT == "D")
                {
                    sql = "Exec RptClearngRegister '" + BranchID + "' ,'1' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "','" + SGL + "'";
                    DT = conn.GetDatatable(sql);
                }
                else if (FLT == "S")
                {
                    sql = "Exec RptClearngReturnRegister '" + BranchID + "','1' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
            }
            else if (FL == "OW")
            {
                if (FLT == "D")
                {
                    sql = "Exec RptClearngRegister '" + BranchID + "' ,'2' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "','" + SGL + "'";
                    DT = conn.GetDatatable(sql);
                }
                else if (FLT == "S")
                {
                    sql = "Exec RptClearngReturnRegister '" + BranchID + "','2' ,'" + FBKcode + "','" + conn.ConvertDate(FDT) + "'";
                    DT = conn.GetDatatable(sql);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
        return DT;
    }
}