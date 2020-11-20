using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsLogMaintainance
/// </summary>
public class 
    ClsLogMaintainance//Dhanya Shetty Class to maintain logs//
{
    string sql = "", Sts="";
    int res ;
     DbConnection conn = new DbConnection();
	public ClsLogMaintainance()
	{

	}
    public string LOGDETAILS(string FL, string BRCD, string VID, string ACTIVITY,string NEWVALUE ,string MID)
    {
        int RES = 0;
        try
        {
            if (FL == "Insert")
            {
                sql = "Exec SP_LOGDETAILS @flag='" + FL + "',@BRCD='" + BRCD + "',@VID='" + VID + "',@ACTIVITY='" + ACTIVITY + "',@NEWVALUE ='" + @NEWVALUE + "',@MID='" + MID + "'";
               // Sts = conn.sExecuteScalar(sql); --Commented for SP return Integer value not String Value and resulting into null Output (Problem for Cut Creation) --Abhihsek 29/05/2017
                RES = conn.sExecuteQuery(sql);
                
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        //return Sts;
        return RES.ToString();
    }
   
    public string GETAUTHORISED(string ENTRYDATE,string SUBGLCODE,string ACCNO,string BRCD)
    {
        try
        {

            sql = "Exec SP_AUTHORISEDPRINT '"+conn.ConvertDate(ENTRYDATE)+"','"+SUBGLCODE+"','"+ACCNO+"','"+BRCD+"'";
                Sts = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Sts;
    }
    public int updatedepositinfo(string remarks, string SUBGLCODE, string ACCNO, string BRCD)
    {
        
        try
        {

            sql = "update  depositinfo set REMARK='"+remarks+"' where brcd='"+BRCD+"' and depositglcode='"+SUBGLCODE+"' and custaccno='"+ACCNO+"'";
             res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }
 public string LOGDETAILSCust(string FL, string BRCD, string VID, string ACTIVITY, string OLDVALUE, string NEWVALUE, string MID)// ANKITA 24/06/2017
    {
        int RES = 0;
        try
        {
            if (FL == "Insert")
            {
                sql = "Exec SP_LOGDETAILS @flag='" + FL + "',@BRCD='" + BRCD + "',@VID='" + VID + "',@ACTIVITY='" + ACTIVITY + "',@OLDVALUE='"+OLDVALUE+"',@NEWVALUE ='" + @NEWVALUE + "',@MID='" + MID + "'";
                RES = conn.sExecuteQuery(sql);

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        //return Sts;
        return RES.ToString();
    }
 public int ChkLogExist(string BRCD, string acd, string uname)
 {
     int RES = 0;
     try
     {
         sql = "select count(*) from AVS500 where ACTIVITY='PC-DB-Created_Upload_" + acd + "_" + uname + "' and convert(varchar(10),ENTRYDATE,121) = CONVERT(varchar(10),getdate(),121) and BRCD='" + BRCD + "'";
         RES = Convert.ToInt32(conn.sExecuteScalar(sql));
     }
     catch (Exception Ex)
     {
         ExceptionLogging.SendErrorToText(Ex);
     }
     //return Sts;
     return RES;
 }
}
