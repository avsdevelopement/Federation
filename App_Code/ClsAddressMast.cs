using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public class ClsAddressMast
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public ClsAddressMast()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int BindADD(GridView Gview, string cstno,string BRCD)
    {
        try 
        {
        string sql = "SELECT A.SRNO,A.CUSTNO,A.ADDRESS,A.STREET_SECTOR,A.NEAR,A.STATE,A.AREA_TALUKA,A.DISTRICT, A.PINCODE,M.CUSTNAME,LNO1.Description  FROM ADDMAST A" +
                 " LEFT JOIN (SELECT Description,SRNO FROM LOOKUPFORM1 where lno='1027') LNO1 ON LNO1.SRNO=A.ADDTYPE " +
                 " LEFT JOIN MASTER M ON M.CUSTNO=A.CUSTNO WHERE A.CUSTNO='" + cstno + "' AND A.OPR_STATUS<>3 AND A.STAGE<>'1004'";

        Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return Result;
    }
    public int CheckAdd(string BRCD, string CustNo, string Addtype)//Dhanya shetty//07-10-2017
    {
        try
        {
            sql = " select isnull(count(*),0) from ADDMAST where brcd='" + BRCD + "' and custno='" + CustNo + "' AND STAGE<>1004  AND ADDTYPE='" + Addtype + "' and OPR_STATUS<>'3'  ";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex); ;
        }
        return Result;
    }
}