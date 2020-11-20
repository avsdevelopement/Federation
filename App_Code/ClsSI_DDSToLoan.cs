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
/// Summary description for ClsSI_DDSToLoan
/// </summary>
public class ClsSI_DDSToLoan
{
    DbConnection DBconn = new DbConnection();
    DataTable DT = new DataTable();
    int Result = 0;
    string sql = "";
	public ClsSI_DDSToLoan()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable CreateSIDDStoLoan(string FLAG,string status,string BRCD,string FDT,string TDT)//BRCD ADDED --Abhishek
    {
       
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        try
        {
            //  string tbname;
            //sql = "SELECT AV.DRACCNO DR_ACCNO,MM2.CUSTNAME DR_NAME,AV.CRACCNO CR_ACCNO,MM1.CUSTNAME CR_NAME," +
            //" AV.SIAMOUNT INSTALLMENT,AV.STATUS STATUS,AV.FROMDATE FROM_DATE,AV.TODATE TO_DATE," +
            //" AV.NEXTEXECUTIONDATE NEXT_TRANSFER FROM AVS5007 AV " +
            //" INNER JOIN MASTER MM1 ON AV.CRCUSTNO=MM1.CUSTNO  AND AV.BRCD=MM1.BRCD  " +
            //" INNER JOIN MASTER MM2 ON AV.DRCUSTNO=MM2.CUSTNO  AND AV.BRCD=MM2.BRCD " +
            //" INNER JOIN GLMAST GG ON AV.DRSUBGL=GG.SUBGLCODE AND GG.BRCD=AV.BRCD  " +
            //" WHERE AV.STATUS='" + status + "' and AV.BRCD='" + BRCD + "'";
            sql = "EXEC SP_SI_REPORT @FLAG='" + FLAG + "',@STATUS='" + status + "',@BRCD='" + BRCD + "',@FDATE='" + DBconn.ConvertDate(FDT) + "',@TDATE='" + DBconn.ConvertDate(TDT) + "' ";
            DT = DBconn.GetDatatable(sql);
          
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}