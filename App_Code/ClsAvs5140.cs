using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsAvs5140
/// </summary>
public class ClsAvs5140
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    string TableName = "";
    int Result = 0;

	public ClsAvs5140()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetCustomerBalane (string BrCd, string Ason)
    {
        try
        {
            sql = "Exec SP_CustomerBalane '" + BrCd + "','" + conn.ConvertDate(Ason) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet GetMobileData (string FBrcd, string TBrcd,string FCustNo, string TCustNo,string AsOnDate, string FL, string FLT)
    {
        try
        {
            sql = "Exec RptMobileData @FBrcd='" + FBrcd + "',@TBrcd='" + TBrcd + "',@FCustNo='" + FCustNo + "',@TCustNo='" + TCustNo + "',@AsonDate='" + conn.ConvertDate(AsOnDate) + "',@Flag='" + FL + "',@LiveYN='" + FLT + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet GetAVS51182(string FBrcd, string FPRCD, string FACCNO, string TACCNO, string AsOnDate, string Amt, string SL, string S1, string S2, string S3, string FL, string FLT, string Flag)
    {
        try
        {
            sql = "Exec RptAVS51182 @FBrcd='" + FBrcd + "',@SubgL='" + FPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "',@AsonDate='" + conn.ConvertDate(AsOnDate) + "',@ShrAmt='" + Amt + "',@DepAmt='" + SL + "',@DepPeriod='" + S1 + "',@LoanAmt='" + S2 + "',@LoanPeriod='" + S3 + "',@AttYr='" + FL + "',@AttMin='" + FLT + "',@Flag='" + Flag + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

    public DataSet Get_TDWithdrwalVchr (string BRCD, string Prd, string Acc, string SETNO, string AsOnDate)
    {
        try
        {
            sql = "Exec RptTDVoucherPrint @Brcd='" + BRCD + "',@SubgL='" + Prd + "',@AccNo='" + Acc + "',@SetNo='" + SETNO + "',@EDate='" + conn.ConvertDate(AsOnDate) + "' ";
            DS = new DataSet();
            DS.Tables.Add(conn.GetDatatable(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

    
}
