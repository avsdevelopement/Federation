using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsNPA
/// </summary>
public class ClsNPA
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
    int res = 0, result = 0;
	public ClsNPA()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDetails(string srno)
    {
        try
        {
            sql = "select * from avs5015 where id='" + srno + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int MODIFY(int ID, int GRCODE, string ASSET, string  GFROM, string  GTO, string  PROVSECURED, string  PROVUNSECURED, string  INTSECURED, string INTUNSECURED, string brcd, string MID)
    {
        try
        {
            sql = "EXEC SP_NPACLASS @FLAG='MD',@ID='"+ID+"',@GRCODE='"+GRCODE+"',@ASSET='"+ASSET+"',@GFROM='"+GFROM+"',@GTO='"+GTO+"',@PROVSECURED='"+PROVSECURED+"',@PROVUNSECURED='"+PROVUNSECURED+"',@INTSECURED='"+INTSECURED+"',@INTUNSECURED='"+INTUNSECURED+"',@BRCD='"+brcd+"',@MID='"+MID+"'";

            res = conn.sExecuteQuery(sql);
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int INSERT(int ID, int GRCODE, string ASSET, string GFROM, string GTO, string PROVSECURED, string PROVUNSECURED, string INTSECURED, string INTUNSECURED, string brcd, string MID)
    {
        try
        {
            sql = "EXEC SP_NPACLASS @FLAG='AD',@ID='" + ID + "',@GRCODE='" + GRCODE + "',@ASSET='" + ASSET + "',@GFROM='" + GFROM + "',@GTO='" + GTO + "',@PROVSECURED='" + PROVSECURED + "',@PROVUNSECURED='" + PROVUNSECURED + "',@INTSECURED='" + INTSECURED + "',@INTUNSECURED='" + INTUNSECURED + "',@BRCD='" + brcd + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int DELETE(int ID, int GRCODE, string ASSET, string GFROM, string GTO, string PROVSECURED, string PROVUNSECURED, string INTSECURED, string INTUNSECURED, string brcd, string MID)
    {
        try
        {
            sql = "EXEC SP_NPACLASS @FLAG='DL',@ID='" + ID + "',@GRCODE='" + GRCODE + "',@ASSET='" + ASSET + "',@GFROM='" + GFROM + "',@GTO='" + GTO + "',@PROVSECURED='" + PROVSECURED + "',@PROVUNSECURED='" + PROVUNSECURED + "',@INTSECURED='" + INTSECURED + "',@INTUNSECURED='" + INTUNSECURED + "',@BRCD='" + brcd + "',@MID='" + MID + "'";

            res = conn.sExecuteQuery(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public DataTable BindNPAGrid(GridView grd)
    {
        try
        {
            sql = "SELECT * FROM avs5015 ORDER BY ID";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetNPAReg1(string EDT, string BRCD, string DATE, string FSUBGL,string TSUBGL, string FL, string SFL1, string SFL2)
    {
        try
        {
           if (FL == "NPA")
            {
                sql = "EXEC ISP_OnlyLoanOverdue @Flag='OD',@SFlag='NPA',@SFlag2='" + SFL2 + "',@SFlag1='" + SFL1 + "',@Brcd='" + BRCD + "',@FSbgl='" + FSUBGL + "',@TSbgl='" + TSUBGL + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
            }
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
}