using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsOWGCharges
{

    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public ClsOWGCharges()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int BindParameters(GridView Gview,string BRCD) //BRCD ADDED --Abhishek
    {
        try 
        {
            sql = "select * from OWG_PARAMETER where BRCD='" + BRCD + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public int BindCharges(GridView Gview, string BRCD) //BRCD ADDED --Abhishek
    {
        try 
        {
            sql = "select * from OWG_CHARGES where BRCD='" + BRCD + "' ORDER BY OWGID DESC";
            Result = conn.sBindGrid(Gview,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public int BindClearing(GridView Gview, string smid, string sbrcd,string EDT)
    {
        try 
        {
            string[] TD = EDT.Split('/'); 
            sql ="select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,'' BankName, "+
                " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS,(CASE WHEN OW.STAGE='1001' THEN 'CREATED' WHEN OW.STAGE='1003' THEN 'AUTHORIZE' WHEN OW.STAGE=1005 THEN 'FUNDED' ELSE '' END) STAGE, " +
                " (CASE WHEN OW.CLG_FLAG='1' THEN 'PASS' WHEN CLG_FLAG='4' THEN 'UNPASS' when OW.CLG_FLAG='3' THEN 'RETURN' END) STATUS   from INWORD_"+TD[2].ToString()+TD[1].ToString()+" OW " +
                " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE "+
                " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO "+
               // " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE "+
                " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <> '1004' AND OW.CLG_FLAG in ('1','4','3')  AND OW.CD IN ('C','D' ) AND OW.EntryDate ='" + conn.ConvertDate(EDT) + "' and OW.PRDUCT_CODE NOT IN(503,504) and ow.INSTRU_NO<>0 order by OW.SET_NO, OW.SCROLL_NO ";
            //AND OW.MID='" + smid + "'
          Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public int BindIdentityProof(GridView Gview,string CUSTNo,string BRCD)
    {
        try 
        {
        sql = "SELECT I.ID,I.CUSTNO,M.CUSTNAME,LNO1.DESCRIPTION NAME,I.DOC_NO,I.DOC_DATE FROM IDENTITY_PROOF I "+
              " INNER JOIN MASTER M ON M.CUSTNO=I.CUSTNO "+
              " LEFT JOIN (SELECT DESCRIPTION,SRNO ID FROM LOOKUPFORM1 WHERE  LNO='1031') LNO1 ON LNO1.ID=I.DOC_TYPE " +
              " WHERE I.CUSTNO='" + CUSTNo + "' AND I.BRCD='" + BRCD + "' AND I.STAGE<>'1004'";
        //Changed from 1024 to 1031 // Dhanya Shetty //26/02/2018
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