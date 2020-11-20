using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsTDAIntMast
/// </summary>
public class ClsTDAIntMast
{
    string sql;
    DbConnection conn = new DbConnection();
    int result = 0,Disp;
	public ClsTDAIntMast()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetGridData(GridView grid, string brcd)
    {
        try
        {
            sql = "select SRNO,DEPOSIT_TYPE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTYPE2,PERIODTO,RATE,PENALTY from A50001 where BRCD='"+brcd+"' and STAGE<>'1004' order by SRNO";
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Disp;
    }
    public int GetFilter(GridView grid,string CT,string PR,string FPRDTYPE,string TPRDTYPE,string FL)
    {
        try
        {
            if (FL == "BRPC")
            {
                sql = "select SRNO,DEPOSIT_TYPE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTYPE2,PERIODTO,RATE,PENALTY,BRCD from A50001 " +
                        " where DEPOSITGL between '" + FPRDTYPE + "' and '" + TPRDTYPE + "'  and STAGE<>'1004' order by SRNO";
            }
            else if (FL == "CT")
            {
                sql = "select SRNO,DEPOSIT_TYPE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTYPE2,PERIODTO,RATE,PENALTY,BRCD from A50001 where DEPOSITGL between '" + FPRDTYPE + "' and '" + TPRDTYPE + "' and TDCUSTTYPE='"+CT+"' and STAGE<>'1004' order by SRNO";
            }
            else if (FL == "PR")
            {
                sql = "select SRNO,DEPOSIT_TYPE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTYPE2,PERIODTO,RATE,PENALTY,BRCD from A50001 where and DEPOSITGL between '" + FPRDTYPE + "' and '" + TPRDTYPE + "' and PERIODTYPE='" + PR + "' and STAGE<>'1004' order by SRNO";
            }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception ex)
        {
              ExceptionLogging.SendErrorToText(ex);
        }
        return Disp;
    }
    public string GetDepositCat(string BRCD, string PRDCODE, string FL)
    {
        try
        {
            sql = "EXEC SP_VALIDATE_CAT @FLAG='" + FL + "',@BRCD=" + BRCD + ",@DEPOGLCODE='" + PRDCODE + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetTDAInfo(string FL, string FPRDTYPE, string TPRDTYPE, string CT, string PR )
    {
        DataTable DT1 = new DataTable();
        try
        {
            //sql = "select SRNO,DEPOSIT_TYPE,TDCUSTTYPE,DEPOSITGL,PERIODTYPE,PERIODFROM,PERIODTYPE2,PERIODTO,RATE,PENALTY,BRCD from A50001 " +
            //            " where BRCD between '" + FBRCD + "' and '" + TBRCD + "'  and DEPOSITGL between '" + FL + "' and '" + FLT + "'  and STAGE<>'1004' order by SRNO";
            sql = "EXEC SP_TDAINTMASTER @flag='" + FL + "',@FDEPOSITGL='" + FPRDTYPE + "',@TDEPOSITGL='" + TPRDTYPE + "',@TDCUSTTYPE='" + CT + "',@PERIODTYPE='" + PR + "'";
            DT1 = new DataTable(); 
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }

    public string GetDepositCat(string GLNAME, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM GLMAST WHERE GLNAME='" + GLNAME + "' AND BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
}