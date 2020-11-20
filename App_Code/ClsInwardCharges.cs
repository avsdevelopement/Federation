using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsInwardCharges 
/// </summary>
public class ClsInwardCharges
{

    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public ClsInwardCharges()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int BindParameters(GridView Gview,string BRCD)
    {
        try
        {
            sql = "select * from OWG_PARAMETER where BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }

    public int BindCharges(GridView Gview,string BRCD)
    {
        try
        {
            sql = "select * from OWG_CHARGES where BRCD='" + BRCD + "' ORDER BY OWGID DESC"; //BRCD ADDED --Abhishek
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public int BindClearing(GridView Gview, string smid, string sbrcd, string EDT)
    {
        try
        {
            string[] TD = EDT.Split('/');
            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
                    " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS from INWORD_" + TD[2].ToString() + TD[1].ToString() + " OW " +
                    " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO AND M.BRCD = ACC.BRCD " +
                    " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                    " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE ='1001' AND OW.CLG_FLAG ='1' AND OW.MID='" + smid + "' AND OW.CD='C' AND OW.EntryDate ='" + conn.ConvertDate(EDT) + "' order by OW.SET_NO, OW.SCROLL_NO ";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public int BindIdentityProof(GridView Gview, string CUSTNo, string BRCD)
    {
        try
        {
            sql = "SELECT I.ID,I.CUSTNO,M.CUSTNAME,LNO1.DESCRIPTION NAME,I.DOC_NO,I.DOC_DATE FROM IDENTITY_PROOF I " +
                  " INNER JOIN MASTER M ON M.CUSTNO=I.CUSTNO AND M.BRCD=I.BRCD " +
                  " LEFT JOIN (SELECT DESCRIPTION,SRNO ID FROM LOOKUPFORM1 WHERE  LNO='1024') LNO1 ON LNO1.ID=I.DOC_TYPE " +
                  " WHERE I.CUSTNO='" + CUSTNo + "' AND I.BRCD='" + BRCD + "' AND I.STAGE<>'1004'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
}