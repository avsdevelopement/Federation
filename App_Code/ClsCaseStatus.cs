using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsCaseStatus
{
    string sql;
    int result=0;
    DbConnection conn = new DbConnection();

	public ClsCaseStatus()
	{
		
	}
    public int Insert(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME,  string DECRETALAMOUNT, string CASESTATUS)
    {
        try
        {
            sql = "insert into AVS_2008(BRCD,CASE_YEAR,CASENO, APPLICTIONDATE ,societyTyp, SOCIETYNAME, SOCIETYADDRESS,CITY,PINCODE,RCNO,RCDATE,DEFAULTERNAME,DECRETALAMOUNT,CASESTATUS,SYSTEMDATE,stage)values('" + BRCD + "','" + CASE_YEAR + "','" + CASENO + "' ,'" + conn.ConvertDate(APPLICTIONDATE) + "','" + societyType + "','" + SOCIETYNAME + "','" + SOCIETYADDRESS + "','" + CITY + "' ,'" + PINCODE + "' ,'" + RCNO + "','" + RCDATE + "','" + DEFAULTERNAME + "','" + DECRETALAMOUNT + "','" + CASESTATUS + "','" + System.DateTime.Now.ToString("dd-MM-yyyy") + "','1001')";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int Modify(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME, string DECRETALAMOUNT, string CASESTATUS)
    {
        try
        {
            sql = "update  avs_2008 set stage='1002' ,APPLICTIONDATE='" + APPLICTIONDATE + "' , SOCIETYNAME='" + SOCIETYNAME + "', SOCIETYADDRESS='" + SOCIETYADDRESS + "',CITY='" + CITY + "',PINCODE='" + PINCODE + "',RCNO='" + RCNO + "',RCDATE='" + RCDATE + "',DEFAULTERNAME='" + DEFAULTERNAME + "',DECRETALAMOUNT='" + DECRETALAMOUNT + "',CASESTATUS='" + CASESTATUS + "' where  BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
            result = conn.sExecuteQuery(sql);                                                                                                                                                                                                  
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int Authorise(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME,  string DECRETALAMOUNT, string CASESTATUS)
    {
        try
        {
            sql = "update  avs_2008 set stage=1003  where stage in(1001,1002)  and  BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
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
                sql = "select BRCD,CASE_YEAR,CASENO, APPLICTIONDATE, societyTyp,SOCIETYNAME,SOCIETYADDRESS,CITY,PINCODE,RCNO,RCDATE,DEFAULTERNAME,DECRETALAMOUNT, CASESTATUS from avs_2008 where BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
    }

    public int Delete(string BRCD, string CASE_YEAR, string CASENO, string APPLICTIONDATE, string societyType, string SOCIETYNAME, string SOCIETYADDRESS, string CITY, string PINCODE, string RCNO, string RCDATE, string DEFAULTERNAME,  string DECRETALAMOUNT, string CASESTATUS)
    {
        try
        {
            sql = "delete from avs_2008  where  BRCD= '" + BRCD + "' and CASE_YEAR='" + CASE_YEAR + "' and CASENO='" + CASENO + "' ";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string GetCaseStatus(string srno)// added by Dhanya Shetty //27/02/2017
    {
        sql = "SELECT DESCRIPTION name,SRNO id FROM LOOKUPFORM1 WHERE LNO='1047' and srno='" + srno + "'";
        srno = conn.sExecuteScalar(sql);
        return srno;
    }
    public int Getinfotable(GridView Gview, string BRCD)
    {
        try
        {

            //sql = " select * from avs_2008 where stage in(1001,1002)";
            sql = "  select A.BRCD,A.CASE_YEAR,A.CASENO,A.APPLICTIONDATE,A.societyTyp,A.SOCIETYNAME,A.RCNO,A.RCDATE,CASESTATUS,A.STAGE,l.DESCRIPTION name from avs_2008 A left join  LOOKUPFORM1 l on a.CASESTATUS=l.SRNO WHERE l.LNO='1047' and A.BRCD='" + BRCD + "' and A.stage<>'1004'";
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

}