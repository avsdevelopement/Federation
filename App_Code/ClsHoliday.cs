using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;


public class ClsHoliday
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql;
    int rtn;
	public ClsHoliday()
	{
	}
    public DataTable Display(GridView GView,string entrydate)
    {
        try
        {
            sql = "Select Id,convert(varchar(10),HOLIDAYDATE,103)as HOLIDAYDATE,REASON from avs1026 where status=1 and HOLIDAYDATE>='" + conn.ConvertDate(entrydate).ToString() + "' and stage<>1004";
            conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
           
        }
        return DT;
    }

    public DataTable Day(GridView GView,string day)
    {
        try
        {
            sql = "Select  distinct(DAY) from avs1026 where status=1 and Day='"+day+"'";
            conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return DT;
    }

    public int insertdata(string date,string reason,string brcd,string mid,string pcmac,string status,string stage)
    {
        try
        {
            sql = "insert into avs1026(HOLIDAYDATE,REASON,BRCD,MID,PCMAC,status,stage) values('" + conn.ConvertDate(date).ToString() + "','" + reason + "','" + brcd + "','" + mid + "','" + pcmac + "','" + status + "','" + stage + "')";
            rtn = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;

    }
    public int InsertWeekly(string day, string Year)
    {
        try
        {
            sql = "Exec Isp_DatesinYears @Day='" + day + "',@Year='" + Year + "'";
            rtn = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
  }

    public int Deletedata(string ID,string holidaydate,string reason)
    {
        try
        {
            sql = "update avs1026 set stage=1004 where id='"+ID+"' and HOLIDAYDATE='" + conn.ConvertDate(holidaydate).ToString() + "'and reason='" + reason + "'";
            rtn = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }
 
}