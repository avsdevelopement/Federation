using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsDDSIntMst
/// </summary>
public class ClsDDSIntMst
{
    int rtn;
    int result = 0;
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    string sql;
	public ClsDDSIntMst()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDDSIntrestMaster(GridView GView, string BRCD)
    {
        try
        {
            sql = "select isnull(Convert(varchar(15),A5.EFFECTDATE,103),'01/01/1900') EFFECTDATE ,A5.SRNO,A5.ID,A5.DEPOSITGL, LK.DESCRIPTION CUSTTYPE,A5.PERIODTYPE,A5.PERIODFROM, A5.PERIODTYPE2, A5.PERIODTO, A5.RATE,A5.PENALTY,A5.AFTERMATROI from interestmaster A5 " +
              " INNER JOIN LOOKUPFORM1 LK ON LK.SRNO=A5.TDCUSTTYPE AND LNO='1016' " +
              " WHERE  STAGE<>'1004' ORDER BY ID ";
            conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
          }
        return dt;
    }
    public int EntryInterest(string TDCUSTTYPE, string DEPOSITGL, string PERIODTYPE, string PERIODFROM, string PERIODTO, string RATE, string PENALTY, string STAGE, string BRCD, string MID, string PCMAC, string EFFECTDATE, string AfterMaturity)
    {
        try
        {

            sql = " INSERT INTO interestmaster (SRNO,TDCUSTTYPE, DEPOSITGL, PERIODTYPE, PERIODFROM, PERIODTO, RATE, PENALTY, STAGE, BRCD, MID, PCMAC, EFFECTDATE,AFTERMATROI) " +
                  " VALUES ((select isnull(max(SRNO)+1,1) FROM interestmaster), '" + TDCUSTTYPE + "', '" + DEPOSITGL + "', '" + PERIODTYPE + "', '" + PERIODFROM + "', '" + PERIODTO + "', '" + RATE + "', '" + PENALTY + "', '" + STAGE + "', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + conn.ConvertDate(EFFECTDATE).ToString() + "','" + AfterMaturity + "')";
            rtn = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
         }
        return rtn;
    }
    public DataTable GetIntrestMasterID(string ID, string BRCD)
    {
        try
        {
            sql = "select isnull(Convert(varchar(15),A5.EFFECTDATE,103),'01/01/1900') EFFECTDATE ,A5.ID, A5.SRNO, A5.TDCUSTTYPE, A5.DEPOSITGL, GM.GLNAME, " +
                    " A5.PERIODTYPE,A5.PERIODFROM, A5.PERIODTYPE2, A5.PERIODTO, " +
                    " A5.RATE,A5.PENALTY,A5.AFTERMATROI from interestmaster A5  " +
                    " INNER JOIN LOOKUPFORM1  LK ON LK.SRNO=A5.TDCUSTTYPE AND LK.LNO='1016'  " +
                    " INNER JOIN GLMAST GM ON A5.DEPOSITGL=GM.SUBGLCODE AND A5.BRCD=GM.BRCD " +
                    " WHERE  A5.BRCD='" + BRCD + "' AND A5.ID = '" + ID + "' AND A5.STAGE<>'1004'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public int ModifyIntrestMaster(string TDCUSTTYPE, string DEPOSITGL, string PERIODTYPE, string PERIODFROM, string PERIODTO, string RATE, string PENALTY, string STAGE, string BRCD, string MID, string PCMAC, string EFFECTDATE, string ID, string AfterMaturity)
    {
        try
        {
            sql = " UPDATE INTERESTMASTER SET TDCUSTTYPE='" + TDCUSTTYPE + "', DEPOSITGL='" + DEPOSITGL + "', PERIODTYPE='" + PERIODTYPE + "', PERIODFROM='" + PERIODFROM + "', PERIODTO='" + PERIODTO + "', RATE='" + RATE + "', PENALTY='" + PENALTY + "', STAGE='1002', BRCD='" + BRCD + "', VID='" + MID + "', PCMAC='" + PCMAC + "', EFFECTDATE='" + conn.ConvertDate(EFFECTDATE) + "',AFTERMATROI='" + AfterMaturity + "'  " +
                  " WHERE BRCD='" + BRCD + "' AND ID = '" + ID + "' AND STAGE<>'1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
           }
        return result;
    }
    public int DeleteIntMast(string BRCD, string ID)
    {
        try
        {
            sql = "UPDATE interestmaster SET STAGE=1004 " +
                    " WHERE BRCD='" + BRCD + "' AND ID = '" + ID + "' AND STAGE<>'1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
         }
        return result;
    }

}