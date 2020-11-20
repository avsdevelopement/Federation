using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsInwCharges
/// </summary>
public class ClsInwCharges
{
    DbConnection conn = new DbConnection();
    string STR = "",sql="";
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsInwCharges()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int BindInwCharges(GridView GRD)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(12),EFFECTDATE,103) EFFECTDATE,CHARGESTYPE,DESCRIPTION, CHARGES,PLACC  FROM CHARGESMASTER WHERE CHARGESTYPE=11";
            Result=conn.sBindGrid(GRD,sql);
        
        }
        catch(Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
            return Result;
    }

    //public int InsertIWCharges(string CHGTYPE,string )
    //{
    //    try
    //    {
    //        sql="INSERT INTO CHARGESMASTER (EFFECTDATE,CHARGESTYPE,DESCRIPTION,CHARGES,PLACC,GLCODE,ST_SUBGL,LASTAPPLY,STATUS) VALUES(GETDATE(),'""','INW RETURN CHARGES',115,0,1022,0,0,100)"
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}
}