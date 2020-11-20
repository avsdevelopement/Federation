using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for ClsMISTransfer
/// </summary>
public class ClsMISTransfer
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result = 0;
    string sql = "";
	public ClsMISTransfer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetMISTRF(GridView grd, string FBRCD, string TBRCD, string PRDCODE, string EDT, string FL)
    {
        
        try
        {
            sql = "EXEC SP_MIS_MULTITRANSFER @FLAG='" + FL + "',@EDT='" + conn.ConvertDate(EDT) + "',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRDCODE='" + PRDCODE + "'";
            Result = conn.sBindGrid(grd,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetDepositCat(string BRCD, string PRDCODE,string FL)
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

    //public int POSTMISTRF(string BRCD, string PRDCODE, string EDT, string FL,string MID)
    //{

    //    try
    //    {
    //        sql = "EXEC SP_MIS_MULTITRANSFER @FLAG='" + FL + "',@EDT='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@FPRDCODE='" + PRDCODE + "',@MID='" + MID + "'";
    //        Result = Convert.ToInt32(conn.sExecuteScalar(sql));
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}

    public string POSTMISTRF(string FBRCD, string TBRCD, string PRDCODE, string EDT, string FL, string MID)
    {

        try
        {
            sql = "EXEC SP_MIS_MULTITRANSFER @FLAG='" + FL + "',@EDT='" + conn.ConvertDate(EDT) + "',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FPRDCODE='" + PRDCODE + "',@MID='" + MID + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
}