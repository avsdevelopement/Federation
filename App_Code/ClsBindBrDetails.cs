using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for ClsBindBrDetails
/// </summary>
public class ClsBindBrDetails
{
    DbConnection conn = new DbConnection();

    string sql = "";
    int Result = 0;

    public ClsBindBrDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void BindBranchDetails(DropDownList ddlBrCode)
    {
        sql = "SELECT CONVERT(VARCHAR(10),BRCD)+'_'+CONVERT(VARCHAR(50),Name) MIDNAME, BRCD id FROM BankName";
        conn.FillDDL(ddlBrCode, sql);
    }

    public string GetBranchno(string STR)
    {
        sql = "SELECT BRCD  from BANKNAME where BANKNAME='" + STR + "' AND BRCD<>0";
        STR = conn.sExecuteScalar(sql);
        return STR;
    }
    public string GetBankcd(string BRCD)
    {
        sql = "select bankcd from bankname where brcd='" + BRCD + "'";
        string BANKCD = conn.sExecuteScalar(sql);
        return BANKCD;
    }
    public string GetECSYN (string BRCD)
    {
        sql = "select LISTVALUE from Parameter where brcd='" + BRCD + "'  And  LISTFIELD ='ECS' ";
        string ECSYN = conn.sExecuteScalar(sql);
        return ECSYN;
    }
    public string getauthorised(string BRCD,string SUBGLCODE,string ACCNO,string EDATE)
    {
        sql = "Exec SP_ClsBalance @BrCode = '"+BRCD+"', @SubGlCode = '"+SUBGLCODE+"', @AccNo = '"+ACCNO+"', @EDate = '"+conn.ConvertDate(EDATE)+"'";
        string result = conn.sExecuteScalar(sql);
        return result;
    }

    public string GetFlag(string Para)
    {
        string result = "";
        try
        {
            sql = "select listValue from parameter where listField='"+Para+"'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public string GetPrintStatus(string brcd, string accno)
    {
        string result = "";
        sql = "SELECT ISNULL (PRINT_STATUS,0) AS PRINT_STATUS FROM SharesInfo where brcd='"+brcd+"' and custaccno='"+accno+"' ";
        result = conn.sExecuteScalar(sql);
        return result;
    }
    public string GetPrintStatusCerti(string brcd, string accno,string cerno)
    {
        string result = "";
        sql = "SELECT ISNULL (PRINT_STATUS,0) AS PRINT_STATUS FROM SharesInfo where brcd='" + brcd + "' and custaccno='" + accno + "' and CERT_NO='" + cerno + "' ";
        result = conn.sExecuteScalar(sql);
        return result;
    }
}