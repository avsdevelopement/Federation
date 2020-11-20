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
/// Summary description for ClsAVS5063
/// </summary>
public class ClsAVS5063
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult = "", TableName = "";
    int result;
    DataTable DT = new DataTable();
	public ClsAVS5063()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int BindGrid(GridView Gview, string EDate, string PT, string brcd)
    {
        try
        {
            if (PT == "")
            {
                sql = "select id,Item_code,Item_name,Type,Qty,Price,Total,Convert(varchar(10),Trxdate,103) as Trxdate,POref,Remark,PORefNo,Convert(varchar(10),PODate,103) as PODate,Convert(varchar(10),Deliverydate,103) as Deliverydate  from AVS5031 where  brcd='" + brcd + "' and Entrydate='" + conn.ConvertDate(EDate) + "' and stage <>1004 and CStatus='IN' order by Item_code  ";
            }
            else
            {
                sql = "select id,Item_code,Item_name,Type,Qty,Price,Total,Convert(varchar(10),Trxdate,103) as Trxdate,POref,Remark,PORefNo,Convert(varchar(10),PODate,103) as PODate,Convert(varchar(10),Deliverydate,103) as Deliverydate  from AVS5031 where  brcd='" + brcd + "' and  Item_code='" + PT + "' and stage <>1004 and CStatus='IN'  order by Item_code ";

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
            sql = "Exec Isp_AVS0058 @id='" + id + "',@flag='" + flag + "',@brcd='" + brcd + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string Checkdate(string itemcode, string itemname, string Pono, string brcd)
    {
        try
        {
            sql = "Select count(*) from AVS5031 where Item_code='" + itemcode + "' and Item_name='" + itemname + "' and brcd='" + brcd + "'  and POref='" + Pono + "' and stage<>1004 and CStatus='IN'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public string CheckMid(string Item_code, string Item_name, string brcd)
    {
        try
        {
            sql = "Select mid from AVS5031 where Item_code='" + Item_code + "' and Item_name='" + Item_name + "' and brcd='" + brcd + "' and stage<>1004 and CStatus='IN'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public int insert(string Item_code, string Item_name, string Type, string Qty, string Price, string Total, string Trxdate, string POref, string Remark, string PORefNo, string PODate, string Deliverydate, string Stock, string BRCD, string Edate, string STAGE, string MID, string flag, string STATUS)
    {
        try
        {
            sql = "Exec Isp_AVS0058 @Itmcode='" + Item_code + "',@Itmname='" + Item_name + "',@type='" + Type + "',@Qty='" + Qty + "',@Price='" + Price + "',@total='" + Total + "',@Trxdate='" + conn.ConvertDate(Trxdate) + "'," +
                " @POref='" + POref + "',@remark='" + Remark + "',@PORefNo='" + PORefNo + "',@PODate='" + conn.ConvertDate(PODate).ToString() + "',@Deliverydate='" + conn.ConvertDate(Deliverydate).ToString() + "',@stock='" + Stock + "'," +
                " @brcd='" + BRCD + "',@Edate='" + conn.ConvertDate(Edate).ToString() + "',@stage='" + STAGE + "',@mid='" + MID + "',@flag='" + flag + "',@STATUS='" + STATUS + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int Modify(string Item_code, string Item_name, string Type, string Qty, string Price, string Total, string Trxdate, string POref, string Remark, string PORefNo, string PODate, string Deliverydate, string Stock, string BRCD, string id, string MID, string flag)
    {
        try
        {
            sql = "Exec Isp_AVS0058 @Itmcode='" + Item_code + "',@Itmname='" + Item_name + "',@type='" + Type + "',@Qty='" + Qty + "',@Price='" + Price + "',@total='" + Total + "',@Trxdate='" + conn.ConvertDate(Trxdate) + "'," +
              " @POref='" + POref + "',@remark='" + Remark + "',@PORefNo='" + PORefNo + "',@PODate='" + conn.ConvertDate(PODate).ToString() + "',@Deliverydate='" + conn.ConvertDate(Deliverydate).ToString() + "',@stock='" + Stock + "'," +
                " @brcd='" + BRCD + "',@id='" + id + "',@mid='" + MID + "',@flag='" + flag + "'";
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
            sql = "Exec Isp_AVS0058 @mid='" + MID + "',@id='" + ID + "',@flag='" + flag + "',@brcd='" + brcd + "'";
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
            sql = "Exec Isp_AVS0058 @mid='" + MID + "',@id='" + ID + "',@flag='" + flag + "',@brcd='" + brcd + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string CheckStage(string Item_code, string Item_name, string brcd)
    {
        try
        {
            sql = "Select Stage from AVS5031 where Item_code='" + Item_code + "' and Item_name='" + Item_name + "' and brcd='" + brcd + "' and CStatus='IN' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public int insertSupp(string BillNo, string SupName, string SupContact, string brcd, string Edate, string Mid)
    {
        try
        {
            sql = "insert into AVS5067(BillNo,SupName,SupMobNo,Stage,BRCD,Entrydate,STATUS,MID,SYSTEMDATE)values('"+BillNo+"','"+SupName+"','"+SupContact+"','1001','"+brcd+"','"+conn.ConvertDate(Edate)+"','1','"+Mid+"',GetDate())";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int InsertStock(string Item_code, string Item_name, string Qty, string Price, string Total, string BRCD, string Entrydate, string MID, string BillNo, string Trxdate,
    string POref, string Remark, string PORefNo, string PODate, string Deliverydate)
    {
        try
        {
            sql = "insert into AVS5031(Item_code,Item_name,Qty,Price,Total,BRCD,Entrydate,STAGE,STATUS,MID,SYSTEMDATE,BillNo,TrxType,Trxdate,POref,Remark,PORefNo,PODate,Deliverydate) "+
                " values('" + Item_code + "','" + Item_name + "','" + Qty + "','" + Price + "','" + Total + "','" + BRCD + "','" + conn.ConvertDate(Entrydate) + "', " +
                " '1001','1','" + MID + "',GetDate(),'" + BillNo + "','1','" + conn.ConvertDate(Trxdate) + "','" + POref + "','" + Remark + "','" + PORefNo + "','" + conn.ConvertDate(PODate) + "','" + conn.ConvertDate(Deliverydate) + "')";
             result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
         return result;
    }
    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' and  glgroup <> 'CBB' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
    public string CheckBill( string DOpur, string billno, string brcd)
    {
        try
        {
            sql = "select count(*) from AVS5031 where stage<>1004 and brcd='" + brcd + "' and billno='" + billno + "' and Trxdate='" + conn.ConvertDate(DOpur) + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public int BindGridDisp(GridView Gview, string DOpur, string billno, string brcd)
    {
        try
        {
            sql = "select  Item_code,Item_name,Qty,Price,Total,Convert(varchar(11),Trxdate,103) Trxdate,POref,Remark,PORefNo,Convert(varchar(11),PODate,103) PODate, "+
                " Convert(varchar(11),Deliverydate,103) Deliverydate,Convert(varchar(11),Entrydate,103) Entrydate,BillNo from AVS5031 where stage<>1004 and brcd='" + brcd + "' "+
                " and billno='" + billno + "' and Trxdate='" + conn.ConvertDate(DOpur) + "'";
            
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int BindVoucher(GridView Gview, string Setno, string Edate, string brcd)
    {
        try
        {
            string[] TD = Edate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "select GlCode,SubGlCode,PartiCulars,Amount,(case when TrxType=1 then 'Cr' when TrxType=2 then 'Dr' end)TrxType,SetNo,InstrumentNo,Convert(varchar(11),InstrumentDate,103)InstrumentDate, "+
                " PayMast from  " + TableName + " where brcd='" + brcd + "' and entrydate='" + conn.ConvertDate(Edate) + "' and setno='" + Setno + "' and stage<>1004 and SubGlCode<>99";

            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public DataTable GetStockReport(string BRCD, string FDate, string TDate, string Edate)
    {
        try
        {
            sql = "EXEC  ISP_AVS0134 @Brcd='" + BRCD + "',@FDate='" + conn.ConvertDate(FDate) + "',@TDate='" + conn.ConvertDate(TDate) + "',@EDate='" + conn.ConvertDate(Edate) + "'";
            DT = conn.GetDatatable(sql);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}