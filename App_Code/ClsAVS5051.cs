using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;


public class ClsAVS5051
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult = "";
    int result;
    DataTable DT = new DataTable();
	public ClsAVS5051()
	{
		
	}
   public int insert(string COLUMNNO, string REC_PRD, string POST_PRD, string EXRECCODE,string SHORTNAME, string SHORTMARATHI, string STAGE,  string MID,string PCMAC,string BRCD,
        string VALUE,string TYPE,string RATE, string flag)
    {
        try
        {
            sql = "Exec Isp_AVS0080 @colno='" + COLUMNNO + "',@RPrd='" + REC_PRD + "',@PPrd='" + POST_PRD + "',@EPrd='" + EXRECCODE + "',@shortn='" + SHORTNAME + "',@SMarathi='" + SHORTMARATHI + "',"+
                " @Stage='" + STAGE + "',@Mid='" + MID + "',@Pcmac='"+PCMAC+"',@brcd='" + BRCD + "',@value='"+VALUE+"',@type='"+TYPE+"',@rate='"+RATE+"',@flag='" + flag + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
   public int Modify(string COLUMNNO, string REC_PRD, string POST_PRD, string EXRECCODE, string SHORTNAME, string SHORTMARATHI,  string MID, string PCMAC, string BRCD,
      string VALUE, string TYPE, string RATE, string flag,string id)
   {
       try
       {
           sql = "Exec Isp_AVS0080 @colno='" + COLUMNNO + "',@RPrd='" + REC_PRD + "',@PPrd='" + POST_PRD + "',@EPrd='" + EXRECCODE + "',@shortn='" + SHORTNAME + "',@SMarathi='" + SHORTMARATHI + "'," +
               "@Mid='" + MID + "',@Pcmac='" + PCMAC + "',@brcd='" + BRCD + "',@value='" + VALUE + "',@type='" + TYPE + "',@rate='" + RATE + "',@flag='" + flag + "',@id='"+id+"'";
           result = conn.sExecuteQuery(sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return result;
   }
   public string CheckMid(string id)
   {
       try
       {
           sql = "Select mid from AVS_RS where id='" + id + "' and stage<>1004";
           sResult = conn.sExecuteScalar(sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return sResult;
   }
   public int Authorise(string MID, string ID, string flag, string brcd)
   {
       try
       {
           sql = "Exec Isp_AVS0080 @Mid='" + MID + "',@id='" + ID + "',@flag='" + flag + "',@brcd='" + brcd + "'";
           result = conn.sExecuteQuery(sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return result;
   }
   public int Delete(string MID, string ID, string flag, string brcd)
   {
       try
       {
           sql = "Exec Isp_AVS0080 @Mid='" + MID + "',@id='" + ID + "',@flag='" + flag + "',@brcd='" + brcd + "'";
           result = conn.sExecuteQuery(sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return result;
   }
   public string CheckStage(string id)
   {
       try
       {
           sql = "Select Stage from AVS_RS where  id='" + id + "'";
           sResult = conn.sExecuteScalar(sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return sResult;
   }
   public int BindGrid(GridView Gview, string PT, string brcd)
   {
       try
       {
           if (PT == "")
           {
               sql = "select id,COLUMNNO,REC_PRD,POST_PRD,EXRECCODE,SHORTNAME,SHORTMARATHI,VALUE,TYPE,RATE  from AVS_RS where  brcd='" + brcd + "'  and stage <>1004  ";
           }
           else
           {
               sql = "select id,COLUMNNO,REC_PRD,POST_PRD,EXRECCODE,SHORTNAME,SHORTMARATHI,VALUE,TYPE,RATE from AVS_RS where  REC_PRD='" + PT + "' and brcd='" + brcd + "' and stage <>1004";

           }
           result = conn.sBindGrid(Gview, sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return result;
   }
   public DataTable GetInfo(string id, string flag, string brcd)
   {
       try
       {
           sql = "Exec Isp_AVS0080 @id='" + id + "',@flag='" + flag + "',@brcd='" + brcd + "'";
           DT = conn.GetDatatable(sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return DT;
   }
}