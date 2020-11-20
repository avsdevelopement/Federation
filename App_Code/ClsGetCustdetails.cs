using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsGetCustdetails
/// </summary>
public class ClsGetCustdetails
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "";
    int Result = 0, i = 0;

	public ClsGetCustdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetCust(string Fdate, string BRCD, string CustNo)
    {
        try
        {
            sql = "Exec RptCustGrpDetails @AsonDate ='" + conn.ConvertDate(Fdate) + "',@BrCd='" + BRCD + "',@CustNo='" + CustNo + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetCustGrpDT (string Fdate, string CustNo)
    {
        try
        {
            sql = "Exec RptCustGrpDetailsList @AsonDate ='" + conn.ConvertDate(Fdate) + "',@CustNo='" + CustNo + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetCDRatioDT_Deatils (string Fdate, string BRCD)
    {
        try
        {
            sql = "Exec RptCDRatioReport @Asondate ='" + conn.ConvertDate(Fdate) + "',@BrCode='" + BRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetCDRatio_Deatils(string Fdate, string BRCD)
    {
        try
        {
            sql = "Exec RptCDRatioReport_DT @Asondate ='" + conn.ConvertDate(Fdate) + "',@BrCode='" + BRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataSet FDInterestDetails (string Fdate, string TDate, string BRCD, string CNO)
    {
        try
        {
            sql = "Exec Isp_AVS0038 @PFDT='" + conn.ConvertDate(Fdate).ToString() + "',@PTDT='" + conn.ConvertDate(TDate).ToString() + "',@BRCD='" + BRCD + "', @CustNo='" + CNO + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

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
    public DataSet TDSDetails(string Fdate, string TDate, string BRCD, string CNO, string FL)
    {
        try
        {
            sql = "Exec RptTDSDetails @PFDT='" + conn.ConvertDate(Fdate).ToString() + "',@PTDT='" + conn.ConvertDate(TDate).ToString() + "',@BRCD='" + BRCD + "', @FCustNo='" + CNO + "' , @TCustNo='" + FL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

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
    public DataSet CustMobileDT(string FBRCD, string TBRCD ,string Fdate, string TDate )
    {
        try
        {
            sql = "Exec RptCustMobile @FBRCD='" + FBRCD + "', @TBRCD='" + TBRCD + "',@FDATE='" + conn.ConvertDate(Fdate) + "',@TDATE='" + conn.ConvertDate(TDate) + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

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
    public DataTable GetGLBALdetails (string Fdate, string FBRCD, string TBRCD)
    {
        try
        {
            sql = "Exec Sp_GLBalanceDataTrf @AsonDate ='" + conn.ConvertDate(Fdate) + "',@FBrcd='" + FBRCD + "',@TBrcd='" + TBRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetGLTopNDPdetails(string Fdate, string FBRCD, string FL, string Prd)
    {
        try
        {
            sql = "Exec RptTopDepositer @AsonDate ='" + conn.ConvertDate(Fdate) + "',@BrCd='" + FBRCD + "',@TopCount='" + FL + "',@Product='" + Prd + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetGLTopNLNVdetails(string Fdate, string FBRCD, string FL, string Prd)
    {
        try
        {
            sql = "Exec RptTopLoanDepositers @AsonDate ='" + conn.ConvertDate(Fdate) + "',@BrCd='" + FBRCD + "',@TopCount='" + FL + "',@Product='" + Prd + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetDailyLessClgBalDetails(string Fdate, string FBRCD, string Prd, string FL)
    {
        try
        {
            sql = "Exec RptDailyBalanceLessThenClg @AsonDate ='" + conn.ConvertDate(Fdate) + "',@BrCd='" + FBRCD + "',@Product='" + Prd + "',@Period='" + FL + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }

    public DataTable GetCustBalWithSurity_DT(string Fdate, string BRCD, string FCustNo, string TCustNo, string Div, string Dept)
    {
        try
        {
            sql = "Exec RptCustBalWithSurity @AsonDate ='" + conn.ConvertDate(Fdate) + "',@BrCd='" + BRCD + "',@FCustNo='" + FCustNo + "',@TCustNo='" + TCustNo + "',@Divsion='" + Div + "',@Deprtment='" + Dept + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    
    
}