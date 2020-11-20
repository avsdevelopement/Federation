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

public class ClsLoanSanctionReport
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "";
    int Result = 0, i = 0;

	public ClsLoanSanctionReport()
	{
		
	}

    public DataSet LoanSanctionData(string FBrCode, string TBrCode, string FPrCode, string TPrCode, string FrAmount, string ToAmt, string FEDate, string TEDate, string Workingdate)
    {
        try
        {
            sql = "Exec RptLoanSanction '" + FBrCode + "','" + TBrCode + "','" + FPrCode + "','" + TPrCode + "','" + FrAmount + "','" + ToAmt + "','" + conn.ConvertDate(FEDate).ToString() + "','" + conn.ConvertDate(TEDate).ToString() + "', '" + conn.ConvertDate(Workingdate).ToString() + "',''";
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
    
    //Ankita on 07/08/2017 to display loan amount wise report
    public DataSet LoanAmountWiseRpt(string fbrcd, string tbrcd, string fprdcd, string tprdcd, string fdate, string tdate, string type, string amt)
    {
        try
        {
            sql = "Exec Isp_AVS0031 '" + fbrcd + "','" + tbrcd + "','" + fprdcd + "','" + tprdcd + "','" + conn.ConvertDate(fdate).ToString() + "','" + conn.ConvertDate(tdate).ToString() + "','" + type + "', '" + amt + "'";
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
    }//ashok misal 20/09/2017 for dividant payble
    public DataSet DividantPayble(string BRCD,string Fdate,string Tdate)
    {
        try
        {
            sql = "Exec Isp_AVS0072 '"+BRCD+"','"+conn.ConvertDate(Fdate)+"','"+conn.ConvertDate(Tdate)+"'";
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
    public DataSet LoanSanctionSumry (string FBrCode, string TBrCode, string FPrCode, string TPrCode, string FrAmount, string ToAmt, string FEDate, string TEDate, string Workingdate)
    {
        try
        {
            sql = "Exec RptLoanSanctionSumry '" + FBrCode + "','" + TBrCode + "','" + FPrCode + "','" + TPrCode + "','" + FrAmount + "','" + ToAmt + "','" + conn.ConvertDate(FEDate).ToString() + "','" + conn.ConvertDate(TEDate).ToString() + "', '" + conn.ConvertDate(Workingdate).ToString() + "',''";
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
    public DataSet DepositSancDT (string FBrCode, string TBrCode, string FPrCode, string TPrCode, string FrAmount, string ToAmt, string FEDate, string TEDate, string Workingdate)
    {
        try
        {
            sql = "Exec RptDepositSanction '" + FBrCode + "','" + TBrCode + "','" + FPrCode + "','" + TPrCode + "','" + FrAmount + "','" + ToAmt + "','" + conn.ConvertDate(FEDate).ToString() + "','" + conn.ConvertDate(TEDate).ToString() + "', '" + conn.ConvertDate(Workingdate).ToString() + "',''";
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
    public DataSet DepositSancSumry(string FBrCode, string TBrCode, string FPrCode, string TPrCode, string FrAmount, string ToAmt, string FEDate, string TEDate, string Workingdate)
    {
        try
        {
            sql = "Exec RptDepositSancSumry '" + FBrCode + "','" + TBrCode + "','" + FPrCode + "','" + TPrCode + "','" + FrAmount + "','" + ToAmt + "','" + conn.ConvertDate(FEDate).ToString() + "','" + conn.ConvertDate(TEDate).ToString() + "', '" + conn.ConvertDate(Workingdate).ToString() + "',''";
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
    public DataSet LienMarkListDT(string FBrCode, string TBrCode, string FPrCode, string TPrCode, string AsOnDate)
    {
        try
        {
            sql = "Exec RptLienMarkList '" + FBrCode + "','" + TBrCode + "','" + FPrCode + "','" + TPrCode + "','" + conn.ConvertDate(AsOnDate).ToString() + "' ";
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
}