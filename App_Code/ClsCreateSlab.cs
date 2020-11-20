using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public class ClsCreateSlab
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;

	public ClsCreateSlab()
	{

	}

    public int InsertData(string Mid, string FromSlab, string ToSlab, string SrNo)
    {
        try
        {
            sql = "Insert Into ODSlabWiseData (UID, FromAmount, TOAmount,SrNo) Values ('" + Mid + "', '" + FromSlab.ToString() + "', '" + ToSlab.ToString() + "', '" + SrNo.ToString() + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string Mid)
    {
        try
        {
            sql = "Delete From ODSlabWiseData ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}