using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for CLSAVS51213
/// </summary>
public class CLSAVS51213
{

    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql, SResult = "";
    string GlCode, AccNo = "", Flag="";
    int Result, ShareAccNo, ShareSuspGl = 0;
	public CLSAVS51213()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insert(string BRCD = null, string CUSTNO = null, string CUSTNAME = null, string OPENINGDATE = null, string MID = null, string PCMAC = null, string MEM_TYPE = null, string MEMBERNO = null, string TOTMEMBERN = null, string FLAT_ROOMNO = null, string ADDRESS = null, string DISTRICT = null, string STATE = null, string AREA_TALUKA = null, string PINCODE = null, string MOBILE1 = null, string EMAIL_ID = null, string COMM_NAME = null, string COM_MOBILE1 = null, string COMM_NAME1 = null, string COM_MOBILE2=null)
    {
        try
        {
           
            {
                sql = " EXEC SP_SocietyReg @Flag = 'AD'  , @BRCD='" + BRCD + "',@CUSTNO='" + CUSTNO + "',@CUSTNM='" + CUSTNAME + "',@OPENINGDATE='" + conn.ConvertDate(OPENINGDATE).ToString() + "',@MID='" + MID + "',@PCNAME='" + PCMAC + "',@TOTMEMBERNO='" + TOTMEMBERN + "',@MEM_TYPE='" + MEM_TYPE + "',@MEMBERNO='" + MEMBERNO + "', " +
                      " @A_State='" + STATE + "',@A_District='" + DISTRICT + "',@A_Taluka='" + AREA_TALUKA + "',@A_PinCode='" + PINCODE + "',@A_Ward='" + FLAT_ROOMNO + "',@Address='"+ADDRESS+"' ," +
                      " @C_Mob1='" + MOBILE1 + "',@C_EmailId='" + EMAIL_ID + "',@COMITNAME1='" + COMM_NAME + "' ,@MOBILE='" + COM_MOBILE1 + "' ,@COMMITNAME2='" + COMM_NAME1 + "',@MOBILE1='" + COM_MOBILE2+ "'";
            }  
            Result = conn.sExecuteQuery(sql);
      
        }
    
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int AUTHORIZEDATA(string BRCD = null, string CUSTNO = null)
    {
        try
        {

            {
                sql = "EXEC SP_SocietyReg @Flag = 'AT'  , @BRCD='" + BRCD + "',@CUSTNO='" + CUSTNO + "'";
            }
            Result = conn.sExecuteQuery(sql);

        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int DELETEDATA(string BRCD = null, string CUSTNO = null, string CUSTNAME = null, string OPENINGDATE = null, string MID = null, string PCMAC = null, string MEM_TYPE = null, string MEMBERNO = null, string TOTMEMBERN = null, string FLAT_ROOMNO = null, string ADDRESS = null, string DISTRICT = null, string STATE = null, string AREA_TALUKA = null, string PINCODE = null, string MOBILE1 = null, string EMAIL_ID = null, string COMM_NAME = null, string COM_MOBILE1 = null, string COMM_NAME1 = null, string COM_MOBILE2 = null)
    {
        try
        {

            {
                sql = "EXEC SP_SocietyReg @Flag = 'DL'  , @BRCD='" + BRCD + "',@CUSTNO='" + CUSTNO + "',@CUSTNM='" + CUSTNAME + "',@OPENINGDATE='" + conn.ConvertDate(OPENINGDATE).ToString() + "',@MID='" + MID + "',@PCNAME='" + PCMAC + "',@TOTMEMBERNO='" + TOTMEMBERN + "',@MEM_TYPE='" + MEM_TYPE + "',@MEMBERNO='" + MEMBERNO + "', " +
                      " @A_State='" + STATE + "',@A_District='" + DISTRICT + "',@A_Taluka='" + AREA_TALUKA + "',@A_PinCode='" + PINCODE + "',@A_Ward='" + FLAT_ROOMNO + "',@Address='" + ADDRESS + "' ," +
                      " @C_Mob1='" + MOBILE1 + "',@C_EmailId='" + EMAIL_ID + "',@COMITNAME1='" + COMM_NAME + "' ,@MOBILE='" + COM_MOBILE1 + "' ,@COMMITNAME2='" + COMM_NAME1 + "',@MOBILE1='" + COM_MOBILE2 + "'";
            }
            Result = conn.sExecuteQuery(sql);

        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetInfo(string CustNo,string brcd)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec SP_SocietyReg @brcd = '" + brcd + "', @CustNo = '" + CustNo + "', @Flag = 'VW'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

}