using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsLastNoAccNoupdate
/// </summary>
public class ClsLastNoAccNoupdate
{
    DbConnection conn = new DbConnection();
    int result = 0;
    string sql = "";
	public ClsLastNoAccNoupdate()
	{
		
	}
    public int LastNoUpdate(string BRCD)
    {
        try
        {
             sql = "EXEC SP_UpdateLastNo  @BrCode='" +BRCD+ "'";
             result = conn.sExecuteQuery(sql);
         }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
}
    public int AccNoUpdate(string BRCD, string PRD, string ACC, string GLCODE)
    {
        try
        {
            sql = "EXEC SP_LASTNO_UPDATE @BRCD='" + BRCD + "',@SUBGLCODE='" + PRD + "',@ACCNO='" + ACC + "',@GLCODE='" + GLCODE + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}