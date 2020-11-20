using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsPTRegister
/// </summary>
public class ClsPTRegister
{
    string sql="",StrResult="";
    int IntResult=0;
    DbConnection Conn=new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
	public ClsPTRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetPtRegister(string FL,string EDT, string BRCD, string MM, string YY,string BANKCD,string Div , string Dep)
    {
        try
        {
            if (BANKCD != "1009")
            {
                sql = "Exec Isp_PTRegister_X @Flag='" + FL + "',@MM='" + MM + "',@YY='" + YY + "',@Edt='" + Conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "',@Divsion='" + Div + "',@Deprtment='" + Dep + "' ";
            }
            else
            {
                sql = "Exec Isp_PTRegister_X_1009 @Flag='" + FL + "',@MM='" + MM + "',@YY='" + YY + "',@Edt='" + Conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "' ";
            }
            DT = Conn.GetDatatable(sql);

            if(DT.Rows.Count>0)
                DS.Tables.Add(DT);
            else
                DS=null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataTable GetDemandRec_DT (string BRCD, string Month, string Year, string FL, string FLT)
    {
        try
        {
            sql = "EXEC RptDemandRecList @MM='" + Month + "',@YY='" + Year + "',@Brcd='" + BRCD + "',@Divsion='" + FL + "',@Deprtment='" + FLT + "' ";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataSet GetRecoveryStatement_Total(string ASONDT, string BRCD, string MM, string YY, string BKCD, string Div, string Dep)
    {
        try
        {
            sql = "Exec RptRecSAPNOList @ForMM='" + MM + "',@ForYY='" + YY + "',@RecDiv='" + Div + "',@RecCode='" + Dep + "',@OnDate='" + Conn.ConvertDate(ASONDT) + "',@Brcd='" + BRCD + "' ";
            DT = Conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataTable GetDemandRec_DTREC(string BRCD, string Month, string Year, string FL, string FLT)
    {
        try
        {
            sql = "EXEC RptDemandRecList_DT @MM='" + Month + "',@YY='" + Year + "',@Brcd='" + BRCD + "',@Divsion='" + FL + "',@Deprtment='" + FLT + "' ";
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
}