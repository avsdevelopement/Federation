using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;


/// <summary>
/// Summary description for ClsLoanApplication
/// </summary>
public class ClsLoanApplication
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

    string sql = "";
    int Result = 0;
	public ClsLoanApplication()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void BindBRANCHNAME(DropDownList DDL, string BRCD)
    {
        sql = "SELECT Convert(varchar(100),BRCD)+'-'+Convert(varchar(100),MIDNAME) name,BRCD id from BANKNAME WHERE BRCD<>0 ORDER BY BRCD";
        conn.FillDDL(DDL, sql);
    }
    public int BINDDETAILS(GridView Gview)
    {
        sql = "SELECT SRNO,LOANEENAME,PRODUCTTYPE,LOANDEMAND,SANCTIONAMT FROM LOANDETAILS WHERE STAGE in (1001,1002)";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;
    }
    public DataTable GetPreSanLoanAPPList(string BRCD, string FDate, string TDate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "EXEC RptPreSanLoanAPPList @Brcd ='" + BRCD + "',@FromDate='" + conn.ConvertDate(FDate) + "',@ToDate='" + conn.ConvertDate(TDate) + "' ";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable DTGetSanLoanAPPList (string BRCD, string FDate, string TDate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "EXEC RptSanLoanAPPList @Brcd ='" + BRCD + "',@FromDate='" + conn.ConvertDate(FDate) + "',@ToDate='" + conn.ConvertDate(TDate) + "' ";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
}