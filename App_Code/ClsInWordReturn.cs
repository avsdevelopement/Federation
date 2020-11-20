using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for ClsInWordReturn
/// </summary>
public class ClsInWordReturn
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();

    string sql = "";
    int Result;
	public ClsInWordReturn()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Getinfotable(GridView Gview, string smid, string sbrcd)
    {
        try
        {
            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName,  OW.INSTRU_NO InstNo, UM.LOGINCODE maker, OW.PARTICULARS, CONCAT(CONCAT(OW.SET_NO,'-'),OW.SCROLL_NO) as setscroll from OWG_201607 OW  " +
                    " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                    " LEFT JOIN USERMASTER UM ON UM.PERMISSIONNO=OW.MID AND UM.BRCD=OW.BRCD " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO AND M.BRCD = ACC.BRCD   " +
                    " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE  " +
                    " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE ='1003' AND CLG_FLAG = '1' AND OW.MID='" + smid + "' AND OW.CD='C' AND TO_CHAR(OW.SYSTEM_DATE), 'DD-MM-YYYY') = TO_CHAR(SYSDATE), 'DD-MM-YYYY') order by OW.SET_NO, OW.SCROLL_NO";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }    
}