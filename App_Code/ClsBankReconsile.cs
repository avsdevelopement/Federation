using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsBankReconsile
/// </summary>
public class ClsBankReconsile
{
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    DataSet DS = new DataSet();

    string sql="";
    string Results = "";
	public ClsBankReconsile()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetBranchName(string BRCD)
    {
        try
        {
            sql = "select midname from bankname where brcd='"+BRCD+"'";
            Results = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Results;
    }
    public DataSet getRecodetails(string FBC, string PT, string FDate, string TDate, string FL)
    {
        try
        {
            string fmonth, fyear;
            string tmonth, tyear;

            string[] fdate = FDate.ToString().Split('/');
            fmonth = fdate[1].ToString();
            fyear = fdate[2].ToString();

            string[] tdate = TDate.ToString().Split('/');
            tmonth = tdate[1].ToString();
            tyear = tdate[2].ToString();

            sql = "Exec Sp_BankReconDetails @pfmonth='" + fmonth + "',@ptmonth='" + tmonth + "',@PFDT='" + conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + conn.ConvertDate(TDate).ToString() + "',@pfyear='" + fyear + "',@ptyear='" + tyear + "',@pat='" + PT + "',@BRCD='" + FBC + "',@Flag='" + FL + "'";
            dt = conn.GetDatatable(sql);

            if (dt.Rows.Count > 0)
                DS.Tables.Add(dt);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataTable getdetails(string FL, string BRCD, string FDATE, string TDATE,string ProdCode)
    {
        try
        {
            sql = "exec SP_BankReconsileReport @TYPE='" + FL + "',@BRCD='" + BRCD + "',@FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@ProdCode='" + ProdCode + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataSet getRecodetails_Clear(string FBC, string PT, string FDate, string TDate, string FL)
    {
        try
        {
            string fmonth, fyear;
            string tmonth, tyear;

            string[] fdate = FDate.ToString().Split('/');
            fmonth = fdate[1].ToString();
            fyear = fdate[2].ToString();

            string[] tdate = TDate.ToString().Split('/');
            tmonth = tdate[1].ToString();
            tyear = tdate[2].ToString();

            sql = "Exec Sp_BankReconDt_Clear @pfmonth='" + fmonth + "',@ptmonth='" + tmonth + "',@PFDT='" + conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + conn.ConvertDate(TDate).ToString() + "',@pfyear='" + fyear + "',@ptyear='" + tyear + "',@pat='" + PT + "',@BRCD='" + FBC + "',@Flag='" + FL + "'";
            dt = conn.GetDatatable(sql);

            if (dt.Rows.Count > 0)
                DS.Tables.Add(dt);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public string GetPass(string FL, string BRCD, string FDATE, string TDATE, string ProdCode,string Type)
    {
        string bal = string.Empty;
        try
        {
            sql = "exec SP_BANKPASSBOOK @TYPE='" + FL + "',@BRCD='" + BRCD + "',@FDATE='" + conn.ConvertDate(FDATE) + "',@TDATE='" + conn.ConvertDate(TDATE) + "',@ProdCode='" + ProdCode + "',@Flag='"+Type+"'";
            bal = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return bal;
    }
}