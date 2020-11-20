using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsAVS5093
/// </summary>
public class ClsAVS5093
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    int ResInt = 0;
    string sql = "", ResStr = "";

	public ClsAVS5093()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int BindSureties(GridView Gd, string FL, string Custno,string Subgl,string Accno, string Brcd)
    {
        try
        {
            sql = "Exec Isp_AVS0130 @Flag='" + FL + "',@Custno='" + Custno + "',@Subglcode='" + Subgl + "',@Accno='" + Accno + "',@Brcd='" + Brcd + "'";
            ResInt = Conn.sBindGrid(Gd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
    public int BindSuretiesDeduct(GridView Gd, string FL, string Custno,string Brcd)
    {
        try
        {
            sql = "Exec Isp_AVS0130 @Flag='" + FL + "',@Custno='" + Custno + "',@Brcd='" + Brcd + "'";
            ResInt = Conn.sBindGrid(Gd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
    public int BindLoanAcc(GridView Gd, string FL, string Custno, string Brcd)
    {
        try
        {
            sql = "Exec Isp_AVS0130 @Flag='" + FL + "',@Custno='" + Custno + "',@Brcd='" + Brcd + "'";
            ResInt = Conn.sBindGrid(Gd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
    public int FnInsert_Sure(string FL,string Brcd,string RecDiv,string RecCode,string Custno,string Cname,string Subgl,string Accno,string S_Custno,string S_MemNo,string S_Cname,string Priamt,string Intramt,string StDt,string Status,string Mid,string Edt)
    {
        try
        {
            sql = "Exec Isp_AVS0130 @Flag='" + FL + "',@Brcd='" + Brcd + "',@RecDiv='" + RecDiv + "',@RecCode='" + RecCode + "',@Custno='" + Custno + "',@CustNm='" + Cname + "',@Subglcode='" + Subgl + "',@Accno='" + Accno + "',@S_Custno='" + S_Custno + "',@S_Memno='" + S_MemNo + "',@S_Custnm='" + S_Cname + "',@PriAmt='" + Priamt + "',@IntAmt='" + Intramt + "',@StartDt='" + Conn.ConvertDate(StDt) + "',@ServiceStatus='" + Status + "',@Mid='" + Mid + "',@Edt='" + Conn.ConvertDate(Edt) + "'";
            ResInt = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }

    public int FnManu_DelSurety(string FL,string Id, string Mid, string Edt)
    {
        try
        {
            sql = "Exec Isp_AVS0130 @Flag='" + FL + "',@Id='" + Id + "',@Mid='" + Mid + "',@Edt='" + Conn.ConvertDate(Edt) + "'";
            ResInt = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
    public int FnManu_ModSurety(string FL, string Id, string Mid, string Edt,string Enddt,string Status,string PriAmt,string Intramt)
    {
        try
        {
            sql = "Exec Isp_AVS0130 @Flag='" + FL + "',@Id='" + Id + "',@Mid='" + Mid + "',@Edt='" + Conn.ConvertDate(Edt) + "',@EndDt='" + Conn.ConvertDate(Enddt) + "',@ServiceStatus='" + Status + "',@PriAmt='" + PriAmt + "',@IntAmt='" + Intramt + "'";
            ResInt = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ResInt;
    }
     public DataTable GetDepartment(string FL, string Custno,string Brcd)
    {
        try
        {
            sql = "Exec Isp_AVS0130 @Flag='" + FL + "',@Custno='" + Custno + "',@Brcd='" + Brcd + "'";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    
}