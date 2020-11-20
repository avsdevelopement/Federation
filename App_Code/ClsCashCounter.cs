using System;
using System.Collections.Generic;
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
/// <summary>
/// Summary description for ClsCashCounter
/// created by ashok misal..for Cash counter Managment
/// </summary>
public class ClsCashCounter
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult="";
    int Result = 0;
	public ClsCashCounter()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //Session["BRCD"].ToString(),Session["UGRP"].ToString(),txtusercode.Text, txtusername.Text, ddltype.SelectedValue, txtdebitlimit.Text, txtcreditlimit.Text, conn.PCNAME(),Session["MID"].ToString()
    public int InsertData(string usercode, string username, string type, string cashdebitlimit, string cashcreditlimit,string PCNAME,string MID,string BRCD)
    {
        sql = "insert into AVS5010(USERCODE,USERNAME,TYPE,CASHDEBITLIMIT,CASHCREDITLIMIT,SYSTEMDATE,BRCD,STAGE,PCMAC,MID)values('" + usercode + "','" + username + "','" + type + "','" + cashdebitlimit + "','" + cashcreditlimit + "',GETDATE(),'" + BRCD + "',1001,'" + PCNAME + "','" + MID + "')";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int ModifyData(string usercode, string type, string cashdebitlimit, string cashcreditlimit,string PCNAME,string BRCD)//BRCD ADDED --aBHISHEK
    {
        sql = "UPDATE AVS5010 set TYPE='" + type + "',STAGE=1002,PCMAC='" + PCNAME + "',CASHCREDITLIMIT='" + cashcreditlimit + "',CASHDEBITLIMIT='" + cashdebitlimit + "' where USERCODE='" + usercode + "' AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int DeleteData(string usercode, string PCNAME,string BRCD,string Mid) //BRCD ADDED --aBHISHEK
    {
        sql = "UPDATE AVS5010 SET STAGE=1004,VID='" + Mid + "' WHERE USERCODE='" + usercode + "' AND BRCD='" + BRCD + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int bindlimit(GridView Gview, string BRCD)//BRCD ADDED --aBHISHEK
    {

        sql = "select USERCODE,USERNAME,TYPE,CASHCREDITLIMIT,CASHCREDITLIMIT from  AVS5010 WHERE  STAGE NOT IN(1003,1004) AND BRCD='" + BRCD + "'";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;


    }
    public DataTable showdata(string usercode, string BRCD)//BRCD ADDED --aBHISHEK
    {
        DataTable DT = new DataTable();
        sql = "select USERCODE,USERNAME,TYPE,CASHCREDITLIMIT,CASHCREDITLIMIT from  avs5010 where USERCODE='" + usercode + "' and STAGE<>1003 AND BRCD='" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public string getuname(string usercode, string BRCD)//BRCD ADDED --aBHISHEK
    {
        sql = "select USERNAME from USERMASTER where LOGINCODE='" + usercode + "' AND BRCD='" + BRCD + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string checkuser(string usercode, string BRCD)//BRCD ADDED --aBHISHEK
    {
        sql = "select count(*) from USERMASTER where LOGINCODE='" + usercode + "' AND BRCD='" + BRCD + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string CheckGroup(string usercode, string BRCD)//BRCD ADDED --aBHISHEK
    {
        sql = "select USERGROUP from USERMASTER where LOGINCODE='" + usercode + "' AND BRCD='" + BRCD + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public DataTable GETNOTETYPE(string BRCD)//BRCD ADDED --aBHISHEK
    {
        DataTable DT = new DataTable();
        sql = "SELECT NOTE_TYPE FROM AVS5011 WHERE V_TYPE=999 AND BRCD='" + BRCD + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

    public int insertcashdemo(string vtype,string notetype,string mid,string brcd,string pcname)
    {
        sql = "insert into AVS5011(V_TYPE,NOTE_TYPE,TOTAL_VALUE,STAGE,STATUS,MID,BRCD,PCMAC,SYSTEMDATE,NO_OF_NOTES,ENTRYDATE)values('" + vtype + "','" + notetype + "',0,1003,1,'" + mid + "','" + brcd + "','" + pcname + "',getdate(),0,getdate())";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }

    public string GETPNO(string usercode, string BRCD)
    {
        sql = "select PERMISSIONNO from USERMASTER where LOGINCODE='" + usercode + "' AND BRCD='" + BRCD + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }

    public int UPDATE5010(string USERNAME, string BRCD,string Mid)
    {
        sql = "Update AVS5010 Set Stage = 1003,Cid='"+Mid+"' Where UserCode = '" + USERNAME + "' And BrCd = '" + BRCD + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public string CheckEntry(string Usercode,string Brcd)
    {
        try
        {
            sql = "select count(*) from AVS5010 where usercode='" + Usercode + "' and brcd='" + Brcd + "' and stage<>1004";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
}