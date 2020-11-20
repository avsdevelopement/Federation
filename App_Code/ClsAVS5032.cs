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


public class ClsAVS5032
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult="";
    int result;
    DataTable DT = new DataTable();
	public ClsAVS5032()
	{
	}
    public int insert(string Edate, string Glcode, string Subglcode, string FromAmt, string ToAmt, string ROI, string Stage, string MID, string flag, string brcd, string Period, string PenalInt)
    {
        try
        {
            sql = "Exec Isp_AVS0054 @Edate='" + conn.ConvertDate(Edate).ToString() + "',@glcode='" + Glcode + "',@subgl='" + Subglcode + "',@FAmt='" + FromAmt + "',@TAmt='" + ToAmt + "', "+
                " @ROI='" + ROI + "',@Stage='" + Stage + "',@Mid='" + MID + "',@flag='" + flag + "',@brcd='" + brcd + "',@Period='" + Period + "'	,@PenalInt='" + PenalInt + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int Modify(string Edate, string Glcode, string Subglcode, string FromAmt, string ToAmt, string ROI, string MID, string flag, string id, string brcd, string Period, string PenalInt)
    {
        try
        {
            sql = "Exec Isp_AVS0054 @Edate='" + conn.ConvertDate(Edate).ToString() + "',@glcode='" + Glcode + "',@subgl='" + Subglcode + "',@FAmt='" + FromAmt + "',@TAmt='" + ToAmt + "', "+
            " @ROI='" + ROI + "',@Mid='" + MID + "',@flag='" + flag + "',@id='" + id + "',@brcd='" + brcd + "',@Period='" + Period + "'	,@PenalInt='" + PenalInt + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int Authorise(string MID, string ID, string flag, string brcd)
    {
        try
        {
            sql = "Exec Isp_AVS0054 @Mid='" + MID + "',@id='" + ID + "',@flag='" + flag + "',@brcd='" + brcd + "'";
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
            sql = "Exec Isp_AVS0054 @Mid='" + MID + "',@id='" + ID + "',@flag='" + flag + "',@brcd='" + brcd + "'";
            result = conn.sExecuteQuery(sql);
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
            sql = "Exec Isp_AVS0054 @id='" + id + "',@flag='" + flag + "',@brcd='" + brcd + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int BindGrid(GridView Gview, string EDate, string PT, string brcd)
    {
        try
        {
            if (PT == "")
            {
                //EffectDate='" + conn.ConvertDate(EDate) + "' and
                sql = "select id,Subglcode,FromAmt,ToAmt,ROI,Period,PenalInt  from AVS5028 where  brcd='" + brcd + "' and EffectDate='" + conn.ConvertDate(EDate) + "' and stage <>1004  ";
            }
            else
            {
                sql = "select id,Subglcode,FromAmt,ToAmt,ROI,Period,PenalInt  from AVS5028 where  Subglcode='" + PT + "' and brcd='" + brcd + "' and stage <>1004";

            }
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string  CheckMid(string ID)
    {
        try
        {
            sql = "Select mid from AVS5028 where ID='" + ID + "' and stage<>1004";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string CheckStage(string subgl, string frmamt, string todate, string ROI)
    {
        try
        {
            sql = "Select Stage from AVS5028 where Subglcode='" + subgl + "' and FromAmt='" + frmamt + "' and ToAmt='" + todate + "' and ROI='" + ROI + "' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string Checkdate(string subgl, string frmamt, string todate, string ROI)
    {
        try
        {
            sql = "Select count(*) from AVS5028 where Subglcode='" + subgl + "' and FromAmt='" + frmamt + "' and ToAmt='" + todate + "' and ROI='" + ROI + "' and stage<>1004";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string Checkbranch(string Logincode)
    {
        try
        {
            sql = "select count(*) from USERMASTER where brcd='1' and LOGINCODE='" + Logincode + "' and stage<>1004";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
}