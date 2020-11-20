using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsUserMaster   // CREATE CLASS FILE 
{
    DbConnection conn = new DbConnection();
    string sql = "", STR;
    int Result = 0;

	public ClsUserMaster()
	{
		
	}

    public int submitUserMaster(string txtusername,string txtlogincode, string Multibranchaccess, string autolock, string txtmobile, string txtpermission, 
        string BRCD,string MIDCODE, string usergroup, string CSCB,string CSCBL,string CSDB,string CSDBL,string TRFCB,string TRFCBL,string TRFDB,string TRFDBL,
        string RTGSCB,string RTGSCBL,string RTGSDB,string RTGSDBL,string IWCB,string IWCBL,string IWDB,string IWDBL,string OWCB,string OWCBL,string OWDB,string OWDBL,
        string ABBCB,string ABBCBL,string ABBDB,string ABBDBL,string IBTCB,string IBTCBL,string IBTDB,string IBTDBL)
    {
        int RM,USERSTATUS;
        try
        {
            if (usergroup == "1")
                USERSTATUS = 1;
            else
                USERSTATUS = 0;
            sql = "INSERT INTO USERMASTER (USERNAME,LOGINCODE,MULTIBRANCH,AUTOLOCK,USERMOBILENO,PERMISSIONNO,BRCD,MIDCODE,USERGROUP ,STAGE,userstatus,CSCR,CSCRL,CSDR,CSDRL,TRFCR,TRFCRL,TRFDR,TRFDRL,RTGSCR,RTGSCRL,RTGSDR,RTGSDRL,IWCR,IWCRL,IWDR,IWDRL,OWCR,OWCRL,OWDR,OWDRL,ABBCR,ABBCRL,ABBDR,ABBDRL,IBTCR,IBTCRL,IBTDR,IBTDRL) "
                + " VALUES ('" + txtusername + "','" + txtlogincode + "','" + Multibranchaccess + "','" + autolock + "','" + txtmobile + "','" + txtpermission + "','" + BRCD + "','" + MIDCODE + "','" + usergroup + "','1001','" + USERSTATUS + "','" + CSCB + "','" + CSCBL + "','" + CSDB + "','" + CSDBL + "', "
                +" '" + TRFCB + "','" + TRFCBL + "','" + TRFDB + "','" + TRFDBL + "','" + RTGSCB + "','" + RTGSCBL + "','" + RTGSDB + "','" + RTGSDBL + "','" + IWCB + "','" + IWCBL + "','" + IWDB + "','" + IWDBL + "','" + OWCB + "','" + OWCBL + "','" + OWDB + "','" + OWDBL + "', "
                +" '" + ABBCB + "','" + ABBCBL + "','" + ABBDB + "','" + ABBDBL + "','" + IBTCB + "','" + IBTCBL + "','" + IBTDB + "','" + IBTDBL + "')";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }


    public int Modifyusermaster(string id, string BCode, string txtusername, string txtlogincode, string ddusergroup, string autolock, string ddMultibranchaccess,
        string txtmobile, string txtpermission, string CSCB, string CSCBL, string CSDB, string CSDBL, string TRFCB, string TRFCBL, string TRFDB, string TRFDBL,
        string RTGSCB, string RTGSCBL, string RTGSDB, string RTGSDBL, string IWCB, string IWCBL, string IWDB, string IWDBL, string OWCB, string OWCBL, string OWDB, string OWDBL,
        string ABBCB, string ABBCBL, string ABBDB, string ABBDBL, string IBTCB, string IBTCBL, string IBTDB, string IBTDBL,string MID)
    {
        int RM;
        try
        {
            //string logincode, brcd;
            //string[] user;
            //user = id.ToString().Split('_');

            //brcd = user[0].ToString();
            //logincode = user[1].ToString();

            sql = "UPDATE USERMASTER SET USERNAME ='" + txtusername + "', USERGROUP = '" + ddusergroup + "', AUTOLOCK ='" + autolock + "', "
                    + " MULTIBRANCH = '" + ddMultibranchaccess + "', USERMOBILENO ='" + txtmobile + "', BRCD = '" + BCode + "', PERMISSIONNO = '" + txtpermission + "', "
                    + " CSCR = '" + CSCB + "',CSCRL = '" + CSCBL + "',CSDR = '" + CSDB + "',CSDRL = '" + CSDBL + "',TRFCR = '" + TRFCB + "',TRFCRL = '" + TRFCBL + "',TRFDR = '" + TRFDB + "',TRFDRL = '" + TRFDBL + "', "
                    + " RTGSCR = '" + RTGSCB + "',RTGSCRL = '" + RTGSCBL + "',RTGSDR = '" + RTGSDB + "',RTGSDRL = '" + RTGSDBL + "',IWCR = '" + IWCB + "',IWCRL = '" + IWCBL + "',IWDR = '" + IWDB + "',IWDRL = '" + IWDBL + "', "
                    + " OWCR = '" + OWCB + "',OWCRL = '" + OWCBL + "',OWDR = '" + OWDB + "',OWDRL = '" + OWDBL + "',ABBCR = '" + ABBCB + "',ABBCRL = '" + ABBCBL + "',ABBDR = '" + ABBDB + "',ABBDRL = '" + ABBDBL + "', "
                    + " IBTCR = '" + IBTCB + "',IBTCRL = '" + IBTCBL + "',IBTDR = '" + IBTDB + "',IBTDRL = '" + IBTDBL + "', STAGE ='1002',MIDCODE='"+MID+"' WHERE PERMISSIONNO='"+id+"' "; //WHERE BRCD = '" + brcd + "' AND LOGINCODE ='" + logincode + "' ";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int Suspendusermaster(string id,string VID)
    {
        int RM;
        try
        {
            //string logincode, brcd;
            //string[] user;
            //user = id.ToString().Split('_');

            //brcd = user[0].ToString();
            //logincode = user[1].ToString();

            //sql = "UPDATE USERMASTER SET STAGE ='1004' WHERE BRCD = '" + brcd + "' AND LOGINCODE='" + logincode + "' AND STAGE <> '1004' ";
            //changed by ankita on 17/02/2018 to avoid data duplication
            sql = "UPDATE USERMASTER SET STAGE ='1004',VID='"+VID+"' WHERE PERMISSIONNO = '" + id + "' AND STAGE <> '1004' ";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public int authorizeusermaster(string id, string VerifyMid)
    {
        int RM;
        try
        {
            //string logincode, brcd;
            //string[] user;
            //user = id.ToString().Split('_');

            //brcd = user[0].ToString();
            //logincode = user[1].ToString();

            //sql = "UPDATE USERMASTER SET STAGE ='1003', CIDCODE = '" + VerifyMid  + "' WHERE BRCD = '" + brcd + "' AND LOGINCODE='" + logincode + "' AND STAGE <> '1004' ";

            //changed by ankita on 17/02/2018 to avoid data duplication
            sql = "UPDATE USERMASTER SET STAGE ='1003', CIDCODE = '" + VerifyMid + "' WHERE  PERMISSIONNO = '" + id + "' AND STAGE <> '1004' ";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            return RM = 0;
        }
        return RM;
    }

    public DataTable txtChangeUM(string brcd, string bcode, string logincode)
    {
        DataTable dt = new DataTable();
        DbConnection conn = new DbConnection();
        try
        {
            if (brcd == "")
            {
                brcd = bcode;
            }
            string sql = "SELECT * FROM USERMASTER WHERE LOGINCODE = '" + logincode + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return dt;
    }

    public int BindGrid(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT convert(varchar(10),BRCD)+'_'+convert(varchar(10),LOGINCODE) id,*,case when stage=1001 then 'Not Authorized' when stage=1002 then 'Not Authorized' when stage=1003 then 'Normal' else 'Suspended' end as UserStage FROM USERMASTER WHERE BRCD = '" + BRCD + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string id)
    {
        DataTable DT = new DataTable();
        try
        {
            //string logincode, brcd;
            //string[] user;
            //user = id.ToString().Split('_');

            //brcd = user[0].ToString();
            //logincode = user[1].ToString();

            //sql = "SELECT * FROM USERMASTER WHERE BRCD = '" + brcd + "' and LOGINCODE = '" + logincode + "'";
            //changed by ankita on 17/02/2018 to avoid data duplication
            sql = "SELECT * FROM USERMASTER WHERE PERMISSIONNO = '" + id + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public string GetStage(string id)
    {
        try
        {
            //string logincode, brcd;
            //string[] user;
            //user = id.ToString().Split('_');

            //brcd = user[0].ToString();
            //logincode = user[1].ToString();

            //sql = "SELECT STAGE FROM USERMASTER WHERE BRCD = '" + brcd + "' AND LOGINCODE = '" + logincode + "' ";
            //changed by ankita on 17/02/2018 to avoid data duplication
            sql = "SELECT STAGE FROM USERMASTER WHERE PERMISSIONNO = '" + id + "'";
            STR = conn.sExecuteScalar(sql);
        }   
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return STR ;
    }

    public string ChkUsrExists(string brcd, string logincode)
    {
        try
        {
            sql = "SELECT LOGINCODE FROM USERMASTER WHERE BRCD = '" + brcd + "' AND LOGINCODE = '" + logincode + "' ";
            STR = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return STR;
    }
    
    public string Get_PERMISSIONNO()
    {
        try
        {
            sql = "Select Max(PERMISSIONNO) + 1 From UserMaster";
            STR = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
       return STR;
    }
    public int GetAllDataPerm(GridView grd, string BRCD, string permissionno)
    {
        try
        {
             sql = "SELECT convert(varchar(10),BRCD)+'_'+convert(varchar(10),LOGINCODE) id,* FROM USERMASTER WHERE BRCD = '" + BRCD + "' AND PERMISSIONNO='"+permissionno+"' AND STAGE NOT IN ('1004')";
             Result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int GetAllDataName(GridView grd, string BRCD, string name)
    {
        try
        {
            sql = "SELECT convert(varchar(10),BRCD)+'_'+convert(varchar(10),LOGINCODE) id,* FROM USERMASTER WHERE BRCD = '" + BRCD + "' and USERNAME LIKE '%"+name+"%' AND STAGE NOT IN ('1004')";
            Result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
}

