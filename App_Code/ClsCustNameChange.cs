using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsCustNameChange
/// </summary>
public class ClsCustNameChange
{
    DbConnection conn = new DbConnection();
    int IntRes = 0;
    string sql = "", StrRes = "";
    DataTable DT = new DataTable();
	public ClsCustNameChange()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int UpdateName(string FL, string BRCD, string MID, string CUSTNO,string PREFIX,string CUSTSEX,string FNAME,string MNAME,string LNAME,string EDT)
    {
        try
        {
            sql = "Exec Isp_AVS0040 @Flag='" + FL + "',@Custno='" + CUSTNO + "',@Prefix='" + PREFIX + "',@FName='" + FNAME + "',@MName='" + MNAME + "',@SName='" + LNAME + "',@Gender='" + CUSTSEX + "',@Brcd='" + BRCD + "',@Mid='" + MID + "',@Edt='" + conn.ConvertDate(EDT) + "'";
            IntRes = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntRes;
    }
    public DataTable GetModifiedCustName(string FL,string Brcd, string CUSTNO)
    {
        try
        {
            sql = "Exec Isp_AVS0040 @Flag='" + FL + "',@Brcd='" + Brcd + "',@CustNo='" + CUSTNO + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);    
        }
        return DT;
    }
    
    public int AuthoCustNo(string FL, string BRCD, string MID, string CUSTNO, string PREFIX, string CUSTSEX, string FNAME, string MNAME, string LNAME, string EDT)
    {
        try
        {
            sql = "Exec Isp_AVS0040 @Flag='" + FL + "',@Custno='" + CUSTNO + "',@Prefix='" + PREFIX + "',@FName='" + FNAME + "',@MName='" + MNAME + "',@SName='" + LNAME + "',@Gender='" + CUSTSEX + "',@Brcd='" + BRCD + "',@Mid='" + MID + "',@Edt='" + conn.ConvertDate(EDT) + "'";
            IntRes = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntRes;
    }
    //
    public int BindGrid(GridView GD,GridView GDA,string FL, string Brcd)
    {
        try
        {
            sql = "Exec Isp_AVS0040 @Flag='" + FL + "',@Brcd='" + Brcd + "'";
            IntRes = conn.sBindGrid(GD, sql);

            sql = "Exec Isp_AVS0040 @Flag='BINDAUTHO',@Brcd='" + Brcd + "'";
            IntRes = conn.sBindGrid(GDA, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntRes;
    }
    public DataTable GetAuthorizeData(string FL, string ID)
    {
        try
        {
            sql = "Exec Isp_AVS0040 @Flag='" + FL + "',@Id='" + ID + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int DeleteModifed(string FL, string ID,string MID)
    {
        try
        {
            sql = "Exec Isp_AVS0040 @Flag='" + FL + "',@Id='" + ID + "',@Mid='" + MID + "'";
            IntRes = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntRes;
    }
    
    public string GetMid(string FL, string ID)
    {
        try
        {
            sql = "Exec Isp_AVS0040 @Flag='" + FL + "',@Id='" + ID + "'";
            StrRes = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return StrRes;
    }
}