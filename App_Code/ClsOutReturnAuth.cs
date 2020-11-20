using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for ClsOutReturnAuth
/// </summary>
public class ClsOutReturnAuth
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();

    string sql = "";
    int Result;

	public ClsOutReturnAuth()
	{
	    	
	}

    public int Getinfotable(GridView Gview, string smid, string sbrcd,string EDT,string FL,string SFLAG)
    {
        try 
        {   string tbname="",CD="";

            string[] TD = EDT.Split('/');
            if (FL == "O")
            {
                tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
                CD = "C";
            }
            else if (FL == "I")
            {
                tbname = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                CD = "D";
            }

            if (SFLAG == "OR")
            {
                CD = "D";
            }
            else if (SFLAG == "IR")
            {
                CD = "C";
            }
            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, isnull(M.CUSTNAME,G.GLNAME) Name, "+
		        " OW.INSTRU_AMOUNT Amount,isnull(RBI.DESCR,G.GLNAME) BankName,  OW.INSTRU_NO InstNo, UM.LOGINCODE maker, OW.PARTICULARS, "+
                " Convert(varchar(10),Ow.SET_NO)+'-'+Convert(varchar(10),OW.SCROLL_NO) as setscroll from " + tbname + " OW  " +
                " LEFT JOIN Dbo.AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                " LEFT JOIN Dbo.USERMASTER UM ON UM.PERMISSIONNO=OW.MID AND UM.BRCD=OW.BRCD " +
                " LEFT JOIN Dbo.MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE  " +
                " Left Join DBo.GLMAST G	On OW.PRDUCT_CODE=G.SUBGLCODE and OW.BRCD=G.BRCD "+
                " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE not in(1004,1003) AND OW.CLG_FLAG='3' AND (OW.CD='" + CD + "' or OW.REASON_CODE is null) "+
                " AND OW.ENTRYDATE = '" + conn.ConvertDate(EDT) + "' and OW.PRDUCT_CODE not in (503,504) order by OW.SET_NO, OW.SCROLL_NO";
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