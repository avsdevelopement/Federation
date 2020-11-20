using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;


public class ClsAVS5052
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult = "";
    int result;
    DataTable DT = new DataTable();
    public ClsAVS5052()
    {

    }

    public int insert(string LoanSanction, string Daily, string Monthly, string Locking, string Shares, string Period, string Principal, string Int, string Total, string Req_Principal,
        string Diff, string BRCD, string STAGE, string MID, string PCMAC, string flag)
    {
        try
        {
            sql = "Exec Isp_AVS0081 @Loansanc='" + LoanSanction + "',@Daily='" + Daily + "',@monthly='" + Monthly + "',@Locking='" + Locking + "',@shares='" + Shares + "',@Period='" + Period + "'," +
                "@Princple='" + Principal + "',@Intrs='" + Int + "', @total='" + Total + "',@Req_Prp='" + Req_Principal + "',@Diff='" + @Diff + "',@brcd='" + BRCD + "',@Stage='" + STAGE + "',@Mid='" + MID + "',@Pcmac='" + PCMAC + "',@flag='" + flag + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int Modify(string LoanSanction, string Daily, string Monthly, string Locking, string Shares, string Period, string Principal, string Int, string Total, string Req_Principal,
        string Diff, string flag, string id)
    {
        try
        {
            sql = "Exec Isp_AVS0081 @Loansanc='" + LoanSanction + "',@Daily='" + Daily + "',@monthly='" + Monthly + "',@Locking='" + Locking + "',@shares='" + Shares + "',@Period='" + Period + "'," +
                "@Princple='" + Principal + "',@Intrs='" + Int + "', @total='" + Total + "',@Req_Prp='" + Req_Principal + "',@Diff='" + @Diff + "',@flag='" + flag + "',@id='" + id + "'";
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
            sql = "Select Stage from avs_dds_lock where  id='" + id + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public DataTable GetInfo(string id, string flag)
    {
        try
        {
            sql = "Exec Isp_AVS0081 @id='" + id + "',@flag='" + flag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int Delete(string MID, string ID, string flag)
    {
        try
        {
            sql = "Exec Isp_AVS0081 @Mid='" + MID + "',@id='" + ID + "',@flag='" + flag + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int BindGrid(GridView Gview, string brcd)
    {
        try
        {
            sql = "select id,LoanSanction,Daily,Monthly,Locking,Shares,Period,Principal,Int,Total,Req_Principal,Diff  from avs_dds_lock where  brcd='" + brcd + "'  and stage <>1004  ";
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}