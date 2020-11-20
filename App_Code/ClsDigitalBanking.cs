using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsDigitalBanking
/// </summary>
public class ClsDigitalBanking
{
    DbConnection conn=new DbConnection ();
    DataTable dt = new DataTable();
    string sql=string.Empty;
    string sResult = string.Empty;
	public ClsDigitalBanking()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetBRCD(string BRCD)
    {
        try
        {
            sql = "select midname from bankname where brcd='"+BRCD+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string GetProductName(string BrCode, string ProdCode)
    {
        try
        {
            sql = "Select (IsNull(GlName, '') +'_'+ ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode)) As Name " +
                  "From GlMast Where BrCd = '" + BrCode + "' And SubGlCode ='" + ProdCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public DataTable getTrailBank(string FBRCD, string TBRCD, string ProdCode, string Charges, string PT, string Date, string Flag,string EDATE,string MID)
    {
        try
        {
            sql = "exec SP_Digitalbank @FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@PRODCODE='" + ProdCode + "',@CHARGES='" + Charges + "',@PL='" + PT + "',@DATE='" + conn.ConvertDate(Date) + "',@Flag='" + Flag + "',@EDATE='" + conn.ConvertDate(EDATE) + "',@MID='"+MID+"'";
            dt=conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
}