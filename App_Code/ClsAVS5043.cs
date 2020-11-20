using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// ClsAVS5043 FOR devidend transfer master
/// </summary>
public class ClsAVS5043
{
    DbConnection conn = new DbConnection();
    string sql = "",result="";
    int res = 0;
	public ClsAVS5043()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insert(string CRGL,string CRSUBGL,string CRCUSTNO,string CRACCNO,string DRGL,string DRSUBGL,string DRCUSTNO,string DRACCNO,string MID,string MID_DATE,string BRCD)
    {
        try
        {
            sql = "insert into AVS5033(CRGL,CRSUBGL,CRCUSTNO,CRACCNO,DRGL,DRSUBGL,DRCUSTNO,DRACCNO,MID,MID_DATE,STAGE,BRCD) values('"+CRGL+"','"+CRSUBGL+"','"+CRCUSTNO+"','"+CRACCNO+"','"+DRGL+"','"+DRSUBGL+"','"+DRCUSTNO+"','"+DRACCNO+"','"+MID+"','"+conn.ConvertDate(MID_DATE)+"','1001','"+BRCD+"')";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Modify(string id,string CRGL, string CRSUBGL, string CRCUSTNO, string CRACCNO, string DRGL, string DRSUBGL, string DRCUSTNO, string DRACCNO, string CID, string CI_DATE, string BRCD)
    {
        try
        {
            sql = "update AVS5033 set CRGL='" + CRGL + "',CRSUBGL='" + CRSUBGL + "',CRCUSTNO='" + CRCUSTNO + "',CRACCNO='" + CRACCNO + "',DRGL='" + DRGL + "',DRSUBGL='" + DRSUBGL + "',DRCUSTNO='" + DRCUSTNO + "',DRACCNO='" + DRACCNO + "',CID='" + CID + "',CI_DATE='" + conn.ConvertDate(CI_DATE)+ "',STAGE='1002',BRCD='" + BRCD + "' WHERE ID='" + id + "' AND BRCD='" + BRCD + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Delete(string id,string BRCD)
    {
        try
        {
            sql = "update AVS5033 set STAGE='1004' WHERE ID='" + id + "' AND BRCD='" + BRCD + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Authorise(string id, string BRCD)
    {
        try
        {
            sql = "update AVS5033 set STAGE='1003' WHERE ID='" + id + "' AND BRCD='" + BRCD + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public string getSubgl(string memno)
    {
        try
        {
            sql = "select convert(varchar(10),subglcode)+'_'+convert(varchar(10),custno) from avs_acc where glcode='4' and accno='" + memno + "' and brcd='1'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public string getgldr(string accno,string subgl,string brcd)
    {
        try
        {
            sql = "select convert(varchar(10),glcode)+'_'+convert(varchar(10),custno) from avs_acc where accno='" + accno + "' and subglcode='" + subgl + "' and brcd='" + brcd + "'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public DataTable getdetails(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select ID,CRSUBGL,CRACCNO,DRACCNO,BRCD from AVS5033 where ID='"+id+"' and stage<>'1004'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int binddetails(GridView grd)
    {
        try
        {
            sql = "select ID,CRSUBGL,CRACCNO,DRACCNO,BRCD from AVS5033 where stage<>'1004'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
}