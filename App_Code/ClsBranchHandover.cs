using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsBranchHandover
{
    string sql="";
    int Result = 0;
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
	public ClsBranchHandover()
	{	
	}
    public int BindGrid(string BRCD,string  Edate,string FL,GridView Gview)
    {
        try
        {
            sql = "Exec SP_AUTHOBRANCH @BRCD='" + BRCD + "',@ENTRYDATE='" + conn.ConvertDate(Edate) + "',@EDT='" + conn.ConvertDate(Edate) + "',@FLAG='"+FL+"'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {   
            ExceptionLogging.SendErrorToText(Ex);            
        }
        return Result;
    }
    public int AuthorisedEntry(string BRCD, string EDate, string Flag, string ID,string Type,string MID)
    {
        try
        {
            sql = "EXEC	SP_UPDATEBRANCHAUTHO @FLAG ='" + Flag + "',@TYPE ='" + Type + "',@BRCD ='" + BRCD + "',@ID ='" + ID + "', @MID='" + MID + "',@ENTRYDATE ='" + conn.ConvertDate(EDate) + "',@EDATE='" + conn.ConvertDate(EDate) + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}