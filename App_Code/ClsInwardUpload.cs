using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;



public class ClsInwardUpload
{
    DbConnection conn = new DbConnection();
    string sql="";
    int Result;

	public ClsInwardUpload()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int GetInwardProcess(GridView gdv,string EDT, string IDT, string BRCD, string FL)
    {
        try
        {
            sql = "EXEC SP_INWARD_UPLOAD @DATE='" + conn.ConvertDate(IDT) + "', @FLAG='" + FL + "',@ENTRYDATE='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "'";
            Result = conn.sBindGrid(gdv, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


}