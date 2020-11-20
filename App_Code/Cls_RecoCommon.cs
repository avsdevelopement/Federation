using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_RecoCommon
/// </summary>
public class Cls_RecoCommon
{
    DbConnection Conn = new DbConnection();
    public string STR = "", sql = "";
    public int Res = 0;
    public string BRCD { get; set; }
    public string SUBGLCODE { get; set; }
    public string MID { get; set; }
    public string ASONDT { get; set; }
	public Cls_RecoCommon()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public string FnBL_GetGlName(Cls_RecoCommon CM)
    {
        try
        {
            CM.sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) FROM GLMAST WHERE SUBGLCODE='" + CM.SUBGLCODE + "'  AND BRCD='" + CM.BRCD + "'";
            CM.sql = Conn.sExecuteScalar(CM.sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CM.sql;
    }
    public string FnBl_BBName(Cls_RecoCommon CM)
    {
        try
        {
            sql = "Select Convert(Varchar(500),BANKNAME)+', '+Convert(Varchar(500),MIDNAME) as BBName from BANKNAME where BRCD='" + CM.BRCD + "'";
            sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
}