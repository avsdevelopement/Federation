using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.WebPages;

/// <summary>
/// Summary description for ClsOutAuth
/// </summary>
public class ClsOutAuth
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    string STR = "";
    string sql = "";
    int Result=0;
	public ClsOutAuth()
	{
        		
	}

    public int Getinfotable(GridView Gview, string smid, string sbrcd, string EDT, string FL)//BRCD ADDED --Abhishek
    {
        try 
        {
            string tbname,DC;
            string CONTRA = "";
            tbname = DC = "";
            string[] TD = EDT.Split('/');
            if (FL == "O")
            {
                tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
                CONTRA = "504";
            }
            else if (FL == "I")
            {
                tbname = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                DC = "D";
                CONTRA = "503";
            }

            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName,  OW.INSTRU_NO InstNo, UM.LOGINCODE maker, OW.PARTICULARS,(ConVert(VarChar(10), OW.SET_NO) +'-'+ ConVert(VarChar(10), OW.SCROLL_NO) +'-'+ ConVert(VarChar(10), OW.INSTRU_NO)) As setscroll from " + tbname + " OW  " +
                " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                " LEFT JOIN USERMASTER UM ON UM.PERMISSIONNO=OW.MID AND UM.BRCD=OW.BRCD " +
                " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE  " +
                " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE  in ('1001','1002') AND CLG_FLAG='1'   AND OW.EntryDate ='" + conn.ConvertDate(EDT) + "' and OW.PRDUCT_CODE<>'" + CONTRA + "' order by OW.SET_NO, OW.SCROLL_NO";
            //AND OW.CD='" + DC + "'
        //sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
        //        " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS from OWG_201607 OW " +
        //        " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
        //        " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO AND M.BRCD = ACC.BRCD " +
        //        " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
        //        " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <>'1003' AND OW.STAGE <>'1004' AND OW.MID='" + smid + "' AND OW.CD='C' AND TO_CHAR(OW.SYSTEM_DATE), 'DD-MM-YYYY') = TO_CHAR(SYSDATE), 'DD-MM-YYYY') order by OW.SET_NO, OW.SCROLL_NO ";
        Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public int GetUpassInfo(GridView Gview, string smid, string sbrcd, string EDT, string FL)//BRCD ADDED --Abhishek
    {
        try
        {
            string tbname, DC;
            string CONTRA = "";
            tbname = DC = "";
            string[] TD = EDT.Split('/');
            if (FL == "O")
            {
                tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
                CONTRA = "504";
            }
            else if (FL == "I")
            {
                tbname = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                DC = "D";
                CONTRA = "503";
            }

            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName,  OW.INSTRU_NO InstNo, UM.LOGINCODE maker, OW.PARTICULARS,(CONVERT(VARCHAR(10),OW.SET_NO)+'-'+CONVERT(VARCHAR(10),OW.SCROLL_NO)) as setscroll from " + tbname + " OW  " +
                " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                " LEFT JOIN USERMASTER UM ON UM.PERMISSIONNO=OW.MID AND UM.BRCD=OW.BRCD " +
                " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE  " +
                " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE in ('1001','1002') AND CLG_FLAG='4'   AND OW.EntryDate ='" + conn.ConvertDate(EDT) + "' and OW.PRDUCT_CODE<>'" + CONTRA + "' order by OW.SET_NO, OW.SCROLL_NO";
            //AND OW.CD='" + DC + "'
            //sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
            //        " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS from OWG_201607 OW " +
            //        " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
            //        " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO AND M.BRCD = ACC.BRCD " +
            //        " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
            //        " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <>'1003' AND OW.STAGE <>'1004' AND OW.MID='" + smid + "' AND OW.CD='C' AND TO_CHAR(OW.SYSTEM_DATE), 'DD-MM-YYYY') = TO_CHAR(SYSDATE), 'DD-MM-YYYY') order by OW.SET_NO, OW.SCROLL_NO ";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }


    public int GetinfotableInstNo(GridView Gview, string smid, string sbrcd, string instruno)
    {
        try 
        {
            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName,  OW.INSTRU_NO InstNo, UM.LOGINCODE maker, OW.PARTICULARS, CONCAT(CONCAT(OW.SET_NO,'-'),OW.SCROLL_NO) as setscroll from OWG_201607 OW  " +
                    " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                    " LEFT JOIN USERMASTER UM ON UM.PERMISSIONNO=OW.MID AND UM.BRCD=OW.BRCD " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                    " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE  " +
                    " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <>'1003' AND CLG_FLAG='1' AND INSTRU_NO = '" + instruno + "' AND OW.STAGE <>'1004' AND OW.MID='" + smid + "' AND OW.CD='C' AND TO_CHAR(OW.SYSTEM_DATE), 'DD-MM-YYYY') = TO_CHAR(SYSDATE), 'DD-MM-YYYY') order by OW.SET_NO, OW.SCROLL_NO";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public string GetMID(string SETNO, string SCROLL, string BRCD)
    {
        try
        {
            sql = "SELECT MID FROM OWG_201609 WHERE SET_NO='" + SETNO + "' AND SCROLL_NO='" + SCROLL + "' AND BRCD='" + BRCD + "'";
            STR = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return STR;
        
    }

    public int UpdtLotSt(string flag, string edt, string fset, string tset, string brcd,string mid)
    {
        try
        {
            sql = "exec Isp_Claering_LotPassing @Flag='" + flag + "',@Edt='" + conn.ConvertDate(edt) + "',@FSetno='" + fset + "',@TSetno='" + tset + "',@Mid='"+mid+"',@Brcd='" + brcd + "'";

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
}