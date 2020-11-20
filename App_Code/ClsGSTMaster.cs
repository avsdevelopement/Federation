using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ClsGSTMaster
/// </summary>
public class ClsGSTMaster
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();
	public ClsGSTMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertGSTMaster(string Flag,string Brcd, string GSTPRD, string GST, string CGSTPRD, string CGST, string SGSTPRD, string SGST,string EDATE,string MID)
    {
        try
        {
            sql = "Isp_AVS0090 '" + Flag + "','" + Brcd + "','" + DBconn.ConvertDate(EDATE) + "','" + GSTPRD + "','" + GST + "','" + CGST + "','" + CGSTPRD + "','" + SGST + "','"+SGSTPRD+"','"+MID+"'";
            Result = DBconn.sExecuteQuery(sql);
         

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    
    } 
 public int ModifyGST(string Brcd, string GSTPRD, string GST, string CGSTPRD, string CGST, string SGSTPRD, string SGST,string EDATE,string ID)
    {
        try
        {
            sql = " UPDATE AVS5057 SET BRCD='" + Brcd + "',EFFECTDATE='" + DBconn.ConvertDate(EDATE).ToString() + "',PRDCD='" + GSTPRD + "',GST='" + GST + "',CGST='" + CGST + "',CGSTPRDCD='" + CGSTPRD + "',SGST='" + SGST + "',SGSTPRDCD='" + SGSTPRD + "',STAGE='1002' WHERE ID='" + ID + "'";
            Result = DBconn.sExecuteQuery(sql);
         

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    
    }
    public int DeleteGST(string ID)
    {
        try
        {
            sql = "update AVS5057 set STAGE=1004 WHERE ID='" + ID + "'";
            Result = DBconn.sExecuteQuery(sql);


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }
    public int BindData(GridView grd )
    {
        int Result = 0;

        try
        {
            sql = "SELECT G.ID,G.BRCD,convert(varchar(10),G.EFFECTDATE,103) EFFECTDATE,G.GST,G.PRDCD,M.GLNAME,G.CGST,G.CGSTPRDCD,G.SGST,G.SGSTPRDCD  FROM AVS5057 G INNER JOIN GLMAST M ON G.BRCD=M.BRCD AND M.SUBGLCODE=G.PRDCD WHERE STAGE<>1004";
             Result = DBconn.sBindGrid(grd, sql);


            }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }
    public DataTable GetParameter()
    {
        DataTable Dt = new DataTable();
        try
        {

            sql = "SELECT CGSTPRCD,SGST FROM AVS5056";
        Dt = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public string GetBranchName(string brcd)
    {
        try
        {
            sql = "select MIDNAME from BANKNAME where BRCD='" + brcd + "'";
            sql = DBconn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string GetLoanGL(string acct, string brcd)
    {
        sql = "SELECT GLNAME+'_'+CONVERT(VARCHAR(10),SUBGLCODE) FROM GLMAST WHERE SUBGLCODE='" + acct + "' AND BRCD='" + brcd + "'";
            acct = DBconn.sExecuteScalar(sql);
        return acct;
    }
  
}