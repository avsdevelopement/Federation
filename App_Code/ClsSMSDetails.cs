using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsSMSDetails
/// </summary>
public class ClsSMSDetails
{
    string sql = "";
    string ResultSTR = "";
    int Resutltint = 0;
    DbConnection conn = new DbConnection();
	public ClsSMSDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetSMSPara(string BRCD)
    {
        try
        {
            sql = "EXEC SP_SMS_INSERT @FLAG='PARA',@BRCD='" + BRCD + "'";
            ResultSTR = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResultSTR;

    }
}