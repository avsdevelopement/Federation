using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsParaReg
/// </summary>
public class ClsParaReg
{
	public ClsParaReg()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void paraaddnew(string txtlistfield, string txtlistvalue, string BRCD, string MID)
    {
        DbConnection conn = new DbConnection();
        try 
        {
        string sql = "insert into parameter (LISTFIELD,LISTVALUE,BRCD,MID,CID,VID,STAGE,PCMAC) values ('" + txtlistfield + "','" + txtlistvalue + "','" + BRCD + "','" + MID + "','0','0','1001','" + conn.PCNAME() + "')";
        conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
    }
}