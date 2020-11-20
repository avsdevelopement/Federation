using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsAVS5044
/// </summary>
public class ClsAVS5044
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "", result = "";
    int IntRes = 0;
	public ClsAVS5044()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int BindOutReceipts(GridView GRD,string BRCD, string CUSTNO)
    {
        try
        {
            sql = "select OUTNO,SUBGLCODE,ACCNO from OUTWARD_RECEIPT where BRCD='"+BRCD+"'and CUSTNO='" + CUSTNO + "'  and stage<>'1004'";
           IntRes=Conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntRes;
    }
}