using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


public class ClsActionStatus
{
    string sql;
    int result=0;
    DbConnection conn = new DbConnection();


    public int Insert(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME, string DECRETALAMOUNT, string ACTIONSTATUS)
    {
        try
        {
            sql = "insert into avs_2009(BRCD,CASE_YEAR,CASENO, APPLICTIONDATE ,societyTyp, SOCIETYNAME, SOCIETYADDRESS,CITY,PINCODE,RCNO,RCDATE,DEFAULTERNAME,DECRETALAMOUNT,ACTIONSTATUS,SYSTEMDATE,stage)values('" + BRCD + "','" + CASE_YEAR + "','" + CASENO + "' ,'" + conn.ConvertDate(APPLICTIONDATE) + "','" + societyType + "','" + SOCIETYNAME + "','" + SOCIETYADDRESS + "','" + CITY + "' ,'" + PINCODE + "' ,'" + RCNO + "','" + RCDATE + "','" + DEFAULTERNAME + "','" + DECRETALAMOUNT + "','" + ACTIONSTATUS + "','" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','1001')";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int Modify(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME, string DECRETALAMOUNT, string ACTIONSTATUS)
    {
        try
        {
            sql = "update  avs_2009 set stage='1002' ,APPLICTIONDATE='" + APPLICTIONDATE + "' , SOCIETYNAME='" + SOCIETYNAME + "', SOCIETYADDRESS='" + SOCIETYADDRESS + "',CITY='" + CITY + "',PINCODE='" + PINCODE + "',RCNO='" + RCNO + "',RCDATE='" + RCDATE + "',DEFAULTERNAME='" + DEFAULTERNAME + "',DECRETALAMOUNT='" + DECRETALAMOUNT + "',ACTIONSTATUS='" + ACTIONSTATUS + "' where stage in(1001,1002) and  BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
            result = conn.sExecuteQuery(sql);                                                                                                                                                                                                  
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int Authorise(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME, string DECRETALAMOUNT, string ACTIONSTATUS)
    {
        try
        {
            sql = "update  avs_2009 set stage=1003  where stage in(1001,1002)  and  BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable getdata(string BRCD, string CASE_YEAR, string CASENO)
    {
         DataTable DT = new DataTable();
            try
            {
                sql = "select BRCD,CASE_YEAR,CASENO, APPLICTIONDATE, societyTyp,SOCIETYNAME,SOCIETYADDRESS,CITY,PINCODE,RCNO,RCDATE,DEFAULTERNAME,DECRETALAMOUNT, ACTIONSTATUS from avs_2009 where BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
    }

    public int Delete(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME, string DECRETALAMOUNT, string ACTIONSTATUS)
    {
        try
        {
            sql = "delete from avs_2009  where  BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string GetActionStatus(string srno)// added by Dhanya Shetty //27/02/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='2573'  and SRNO='" + srno + "'";
        srno = conn.sExecuteScalar(sql);
        return srno;
    }
    public int Getinfo(GridView grd, string BRCD)//A.CASE_YEAR='" + CASE_YEAR + "' and A.CASENO='" + CASENO + "' and
    {
        try
        {
           //sql = " select * from avs_2009 where stage in(1001,1002)";
          //  sql = " select A.BRCD,A.CASE_YEAR,A.CASENO,A.APPLICTIONDATE,A.societyTyp,A.SOCIETYNAME,A.SOCIETYADDRESS,A.CITY,A.PINCODE,A.RCNO,A.RCDATE,A.DEFAULTERNAME,A.DECRETALAMOUNT,A.ACTIONSTATUS,A.STAGE,l.DESCRIPTION name,l.SRNO id,A.DecretalName from avs_2009 A left join  LOOKUPFORM1 l on a.ACTIONSTATUS=l.SRNO WHERE l.LNO='2573' and  A.BRCD='" + BRCD + "' and A.stage<> '1004' ";//A.CASE_YEAR='" + CASE_YEAR + "' and A.CASENO='" + CASENO + "' and
            sql = "select A.BRCD,A.CASE_YEAR,A.CASENO,A.APPLICTIONDATE,A.societyTyp,A.SOCIETYNAME,A.RCNO,A.RCDATE,ACTIONSTATUS,A.STAGE,l.DESCRIPTION name from avs_2009 A left join  LOOKUPFORM1 l on a.ACTIONSTATUS=l.SRNO WHERE l.LNO='2573' and  A.BRCD='" + BRCD + "' and A.stage<>'1004' ";
            result = conn.sBindGrid(grd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

}