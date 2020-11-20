using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsInwordAuthoDo
/// </summary>
public class ClsInwordAuthoDo
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    string sql, sqlc, sqld;
    int result;
	public ClsInwordAuthoDo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetFormData(int setno, int scrollno, int brcd)
    {
        try
        {
            sql = "SELECT OW.*,GL.GLNAME PRDNAME,M.CUSTNAME,LNO.DESCRIPTION,GL1.GLNAME ACCTYPE ,GL2.GLNAME OPETYPE,RBI.BANK,RBI2.BRANCH,LNO1.DESCRIPTION OPRTYPE,LNO2.DESCRIPTION ACCTYPEA FROM OWG_201607 OW " +
                        "LEFT JOIN GLMAST GL ON GL.SUBGLCODE=OW.PRDUCT_CODE AND GL.BRCD=OW.BRCD " +
                        "LEFT JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD  " +
                        "LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.BRCD=AC.BRCD  " +
                        "LEFT JOIN GLMAST GL1 ON GL1.SUBGLCODE=OW.ACC_TYPE AND GL1.BRCD=OW.BRCD  " +
                        "LEFT JOIN GLMAST GL2 ON GL2.SUBGLCODE=OW.OPRTN_TYPE AND GL2.BRCD=OW.BRCD " +
                        "LEFT JOIN (SELECT DESCR BANK,BANKRBICD FROM RBIBANK WHERE BRANCHRBICD='0' AND STATECD ='400') RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                        "LEFT JOIN (SELECT DESCR BRANCH,BRANCHRBICD,BANKRBICD FROM RBIBANK WHERE STATECD ='400') RBI2 ON RBI2.BRANCHRBICD=OW.BRANCH_CODE AND RBI2.BANKRBICD=RBI.BANKRBICD " +
                        "LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1017')LNO1 ON LNO1.SRNO=AC.OPR_TYPE " +
                        "LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1016')LNO2 ON LNO2.SRNO=AC.ACC_TYPE " +
                        "LEFT JOIN (SELECT DESCRIPTION,SRNO FROM LOOKUPFORM1 WHERE LNO=1022) LNO ON LNO.SRNO=OW.INSTRU_TYPE WHERE OW.SET_NO='" + setno + "' AND OW.SCROLL_NO='" + scrollno + "' AND OW.BRCD='" + brcd + "' AND OW.STAGE<>'1004' AND OW.STAGE<>'1003' AND OW.CD='C'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }


    public int Authorize(int stage, int setno, int scrollno, int brcd, string MID)
    {
        try
        {
            sql = "Update INWORD_2016010 SET STAGE = '" + stage + "', CID='" + MID + "' WHERE SET_NO='" + setno + "' AND SCROLL_NO='" + scrollno + "' AND BRCD = '" + brcd + "' AND STAGE <> '1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }
    public int cancel(int stage, int setno, int scrollno, int brcd, string MID)
    {
        try
        {
            sql = "Update INWORD_2016010 SET STAGE = '" + stage + "' WHERE SET_NO='" + setno + "' AND SCROLL_NO='" + scrollno + "' AND BRCD = '" + brcd + "' AND STAGE <> '1003'  AND STAGE <> '1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }
    
}