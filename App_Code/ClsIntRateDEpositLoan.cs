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

/// <summary>
/// Summary description for ClsIntRateDEpositLoan
/// </summary>
public class ClsIntRateDEpositLoan
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
	public ClsIntRateDEpositLoan()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetGLname(string brcd, string prcd)
    {
        sql = "select glname from glmast where brcd='"+brcd+"' and subglcode='"+prcd+"'";
        string Res = conn.sExecuteScalar(sql);
        return Res;
    }
    public DataTable GetIntDepositDt(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        try
        {
            if (FL == "DP")
            {
                sql = "Exec RptIntRateSummaryDPList @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + conn.ConvertDate(FDT) + "' ";
                DT = conn.GetDatatable(sql);

            }
            else
            {
                sql = "Exec RptIntRateSummaryLoansList @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + conn.ConvertDate(FDT) + "' ";
                DT = conn.GetDatatable(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetIntDepositDt_DT(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL, string Flag, string FLT)
    {
        try
        {
            if (FL == "DP")
            {
                sql = "Exec RptIntRateSummaryDPList_DT @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + conn.ConvertDate(FDT) + "',@FIntRate='" + Flag + "',@TIntRate='" + FLT + "' ";
                DT = conn.GetDatatable(sql);

            }
            else
            {
                sql = "Exec RptIntRateSummaryLoansList_DT @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + conn.ConvertDate(FDT) + "',@FIntRate='" + Flag + "',@TIntRate='" + FLT + "' ";
                DT = conn.GetDatatable(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    
}