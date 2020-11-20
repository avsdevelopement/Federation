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
/// Summary description for ClsOIRegister
/// </summary>
public class ClsOIRegister
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();
   
	public ClsOIRegister()
        
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable CreateOutward(string FBC,string TBC,string FDT,string TDT, string BRCD,string FL)
    {
        DataTable DT = new DataTable();
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        try
        {
            string tbname;
            string[] TD = FDT.Split('/');

            if (FL == "OW")
            {
                tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
            }
            else
            {
                tbname = "INWORD_" + TD[2].ToString() + TD[1].ToString();
            }
            sql = " SELECT OW.ACC_NO,M.CUSTNAME,OW.PRDUCT_CODE,GL.GLNAME,BANK.DESCR AS BANK_NAME,BRCD.DESCR AS BRANCH_NAME,OW.INSTRU_NO,OW.INSTRUDATE,OW.INSTRU_AMOUNT FROM "+ tbname +" OW " +
                " INNER JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD " +
                " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND AC.BRCD=OW.BRCD " +
                " INNER JOIN GLMAST GL ON GL.GLCODE=AC.GLCODE AND GL.SUBGLCODE=AC.SUBGLCODE " +
                " LEFT JOIN (SELECT DESCR,BANKCD FROM RBIBANK WHERE STATECD=400 AND BRANCHCD=0) BANK ON BANK.BANKCD=OW.BANK_CODE " +
                " LEFT JOIN (SELECT DESCR,BRANCHCD,BANKCD FROM RBIBANK WHERE STATECD=400) BRCD ON BRCD.BANKCD=OW.BANK_CODE AND BRCD.BRANCHCD=OW.BRANCH_CODE " +
                " WHERE OW.STAGE <> 1004 And OW.PRDUCT_CODE Not In (503,504) And OW.ENTRYDATE BETWEEN '" + DBconn.ConvertDate(FDT) + "'AND '" + DBconn.ConvertDate(TDT) + "' AND OW.BRCD BETWEEN '" + FBC + "' AND '" + TBC + "' Order By OW.SET_NO";

           

            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int Insert_data()
    {
        return 0;
    }
    public void DropReport()
    {

    }
    public DataTable GetAccountInfo()
    {
        return DT;
    }
    

}