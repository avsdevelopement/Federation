using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsReturnIO
/// </summary>
public class ClsReturnIO
{
    DbConnection conn = new DbConnection();
    ClsEncryptValue EV = new ClsEncryptValue();
    string sql = "";
    int Result = 0;
    string EMD = "";
    DataTable DT = new DataTable();
	public ClsReturnIO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetInfo(string BRCD,string TYPE, string SBGL, string ACCNO, string CHQNO, string CHQDATE,string EDT)
    {
        try
        {
            sql = "Exec RptClearngReturnDetails '" + BRCD + "','" + TYPE + "','" + conn.ConvertDate(EDT) + "','" + SBGL + "','" + ACCNO + "','" + CHQNO + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int InOutReturn(string Type,string EDT,string GLCODE,string SBGL,string ACCNo,string PARTI,string AMT,string TRXTYPE,string ACT,string SETNO,string PMTMODE,string INSTNO,string INSTDATE,string IBNKCODE,string IBRCD,string stage,string BRCD,string MID,string PAYMAST,string CUSTNO,string CUSTNAME,string CLG_FLAG,string OPR_TYPE,string CD,string REASON_CODE,string SFLAG)
    {
        try
        {
            EMD = EV.GetMK(MID);
            if (SFLAG != "ER")
            {
                sql = "SELECT ISNULL(MAX(SCROLLNO),0)+1 FROM ALLVCR WHERE BRCD='" + BRCD + "' AND SETNO='" + SETNO + "' AND ENTRYDATE='" + conn.ConvertDate(EDT) + "'";
                string SCROLLNO = conn.sExecuteScalar(sql);

                sql = "Exec ClearingReturnTrans_SP '" + SFLAG + "','" + Type + "','" + conn.ConvertDate(EDT) + "','" + conn.ConvertDate(EDT) + "','" + conn.ConvertDate(EDT) + "','" + GLCODE + "','" + SBGL + "','" + ACCNo + "','" + PARTI + "','" + PARTI + "','" + AMT + "','0','" + TRXTYPE + "','" + ACT + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTNO + "','" + conn.ConvertDate(INSTDATE) + "','" + IBNKCODE + "','" + IBRCD + "','" + stage + "','" + BRCD + "','" + MID + "','0','0','','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','0','" + CLG_FLAG + "','0','" + OPR_TYPE + "','" + CD + "','" + REASON_CODE + "','" + EMD + "'";
                Result = conn.sExecuteQuery(sql);
            }
            else
            {
                sql = "SELECT ISNULL(MAX(SCROLLNO),0)+1 FROM ALLVCR WHERE BRCD='" + BRCD + "' AND SETNO='" + SETNO + "' AND ENTRYDATE='" + conn.ConvertDate(EDT) + "'";
                string SCROLLNO = conn.sExecuteScalar(sql);

                sql = "Exec ClearingReturnTrans_SP '" + SFLAG + "','" + Type + "','" + conn.ConvertDate(EDT) + "','" + conn.ConvertDate(EDT) + "','" + conn.ConvertDate(EDT) + "','" + GLCODE + "','" + SBGL + "','" + ACCNo + "','" + PARTI + "','" + PARTI + "','" + AMT + "','0','" + TRXTYPE + "','" + ACT + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTNO + "','" + conn.ConvertDate(INSTDATE) + "','" + IBNKCODE + "','" + IBRCD + "','" + stage + "','" + BRCD + "','" + MID + "','0','0','','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','0','" + CLG_FLAG + "','0','" + OPR_TYPE + "','" + CD + "','" + REASON_CODE + "','" + EMD + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetChqCount(string BRCD, string CHQNO, string FL,string EDT,string ACCT,string ACCNO,string SFL)
    {
        try
        {
            string TBNAME="";
            string[] TD=EDT.Split('/');
            if (FL == "I")
            {
                TBNAME = "INWORD_" + TD[2].ToString() + TD[1].ToString();
            }
            else if (FL == "O")
            {
                TBNAME = "OWG_" + TD[2].ToString() + TD[1].ToString();
            }
            if (SFL == "CH")
            {
                sql = "SELECT count(*) FROM " + TBNAME + " WHERE BRCD='" + BRCD + "' AND INSTRU_NO='" + CHQNO + "' and ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND CLG_FLAG='3' and STAGE<>1004";
            }
            else if (SFL == "ACCH")
            {
                sql = "SELECT count(*) FROM " + TBNAME + " WHERE BRCD='" + BRCD + "' AND INSTRU_NO='" + CHQNO + "' and ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND CLG_FLAG='3' and STAGE<>1004 and PRDUCT_CODE='" + ACCT + "' and ACC_NO='" + ACCNO + "'";
            }
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;

    }
    public int GetReturnEntries(GridView GDR, string smid, string sbrcd, string EDT,string FL,string CD)
    {
        try
        {
            string[] TD = EDT.Split('/');
            string TBNAME = "";
            if (FL == "I")
            {
                TBNAME = "INWORD_" + TD[2].ToString() + TD[1].ToString();
               
            }

            else if (FL == "O")
            {
                TBNAME = "OWG_" + TD[2].ToString() + TD[1].ToString();
        
            }

            sql = "select OW.ENTRYDATE,OW.BRCD,OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
                    " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS,(CASE WHEN OW.STAGE='1001' THEN 'Created' WHEN OW.STAGE='1003' THEN 'Authorized' WHEN OW.STAGE=1005 THEN 'Funded' ELSE '' END) STAGE, " +
                    " (CASE WHEN OW.CLG_FLAG='1' THEN 'Pass' WHEN CLG_FLAG='3' THEN 'Returned' END) STATUS from " + TBNAME + " OW " +
                    " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                    " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                    " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <> '1004' AND OW.CLG_FLAG ='3'  AND OW.CD='" + CD + "' AND OW.EntryDate ='" + conn.ConvertDate(EDT) + "' and OW.PRDUCT_CODE NOT IN(503,504) order by OW.SET_NO, OW.SCROLL_NO ";
       
            Result = conn.sBindGrid(GDR, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}