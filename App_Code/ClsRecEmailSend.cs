using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Web.Util;
using System.Globalization;
using System.IO.Compression;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Web.Util;

/// <summary>
/// Summary description for ClsRecEmailSend
/// </summary>
public class ClsRecEmailSend
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    string StrResult = "", sql = "";
    int IntResult = 0;
	public ClsRecEmailSend()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertData(string FL, string BRCD, string EMAILID, string MID, string EDT)
    {
        try
        {
            sql = "Exec Isp_AVS0107 @Flag='" + FL + "',@EmailId='" + EMAILID + "',@Edt='" + Conn.ConvertDate(EDT) + "',@Mid='" + MID + "',@Brcd='" + BRCD + "'";
            IntResult = Conn.sExecuteQuery(sql);
        }
        catch (System.Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }
    public int BindGrid(GridView GRD,string FL, string BRCD, string EDT)
    {
        try
        {
            sql = "Exec Isp_AVS0107 @Flag='" + FL + "',@Edt='" + Conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            IntResult = Conn.sBindGrid(GRD,sql);
        }
        catch (System.Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }
    public DataTable GetEmailCr(string FL)
    {
        try
        {
            sql = "Exec Isp_AVS0107 @Flag='" + FL + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (System.Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}