using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// 
/// Summary description for CLSIWOWCharges
/// </summary>
public class CLSIWOWCharges
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";
    int Result = 0;
    string STR = "";
	public CLSIWOWCharges()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public  DataTable GetIWOWCharges(string FL)
    {
        try
        {
            if(FL=="I")
                sql = "SELECT DESCRIPTION,CHARGES,PLACC,ST_SUBGL,ISNULL(LASTAPPLY,'') LASTAPPLY FROM CHARGESMASTER WHERE CHARGESTYPE=11 AND STATUS=1";

            else if(FL=="O")
                sql = "SELECT DESCRIPTION,CHARGES,PLACC,ST_SUBGL,ISNULL(LASTAPPLY,'') LASTAPPLY FROM CHARGESMASTER WHERE CHARGESTYPE=12 AND STATUS=1";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetTrialRunGrid(GridView GRD,string FL,string SFL,string BRCD,string EDT,string CHARGES,string MID)
    {
        try
        {
            sql = "EXEC SP_IWOWRETURN_CHARGES @FLAG='" + FL + "',@SFLAG='" + SFL + "',@BRCD='" + BRCD + "',@EDT='" + conn.ConvertDate(EDT) + "',@CHARGES='" + CHARGES + "',@MID='" + MID + "'";
            Result = conn.sBindGrid(GRD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string PostCharges(string FL, string SFL, string BRCD, string EDT, string CHARGES, string MID)
    {
        try
        {
            sql = "EXEC SP_IWOWRETURN_CHARGES @FLAG='" + FL + "',@SFLAG='" + SFL + "',@BRCD='" + BRCD + "',@EDT='" + conn.ConvertDate(EDT) + "',@CHARGES='" + CHARGES + "',@MID='" + MID + "'";
            sql= conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }



    //19/07/2017 Abhishek
    public int BindTrailRun(string FL,GridView GD, string BRCD, string EDT, string MID)
    {
        try
        {
            sql = "EXEC Isp_IOReturnChg_Process @Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            Result = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string PostCharges(string FL, string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "EXEC Isp_IOReturnChg_Process @Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "',@Mid='" + MID + "'";
            STR = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return STR;
    }
    public string GetLastAppliedDate(string BRCD)
    {
        try
        {
            sql = "Select Convert(Varchar(11),isnull(Last_ApplDate,'1900-01-01'),103) as LastApplDate From AVS5018 where BRCD='" + BRCD + "' and STAGE='1003'";
            STR = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return STR;
    }

    public DataTable GetIWOWChargesReport(string FL,string BRCD, string EDT)
    {
        try
        {
            sql = "EXEC Isp_IOReturnChg_Process @Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}