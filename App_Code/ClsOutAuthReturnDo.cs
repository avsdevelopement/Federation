using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsOutAuthReturnDo
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    ClsEncryptValue EV = new ClsEncryptValue();
    string sql;
    int result;
    double CustNo = 0;

    public ClsOutAuthReturnDo()
    {
    }

    public DataTable GetFormDatareturn(int setno, int scrollno, int brcd, string EDT, string FL)
    {
        try
        {
            string tbname, DC;
            tbname = DC = "";
            string[] TD = EDT.Split('/');
            if (FL == "O")
            {
                tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
                DC = "D";
            }
            else if (FL == "I")
            {
                tbname = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
            }
            sql = "SELECT OW.*,GL.GLNAME PRDNAME,isnull(M.CUSTNAME,GL.GLNAME) CUSTNAME,LNO.DESCRIPTION,GL1.GLNAME ACCTYPE ,GL2.GLNAME OPETYPE,isnull(RBI.BANK,GL.GLNAME) BANK,isnull(RBI2.BRANCH,GL.GLNAME) BRANCH,isnull(LNO1.DESCRIPTION,'NA') OPRTYPE,isnull(LNO2.DESCRIPTION,'NA') ACCTYPEA  FROM " + tbname + " OW " +
                    " LEFT JOIN GLMAST GL ON GL.SUBGLCODE=OW.PRDUCT_CODE AND GL.BRCD=OW.BRCD " +
                    " LEFT JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD  " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO " +
                    " LEFT JOIN GLMAST GL1 ON GL1.SUBGLCODE=OW.ACC_TYPE AND GL1.BRCD=OW.BRCD  " +
                    " LEFT JOIN GLMAST GL2 ON GL2.SUBGLCODE=OW.OPRTN_TYPE AND GL2.BRCD=OW.BRCD " +
                    " LEFT JOIN (SELECT DESCR BANK,BANKRBICD FROM RBIBANK WHERE BRANCHRBICD='0' AND STATECD ='400') RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                    " LEFT JOIN (SELECT DESCR BRANCH,BRANCHRBICD,BANKRBICD FROM RBIBANK WHERE STATECD ='400') RBI2 ON RBI2.BRANCHRBICD=OW.BRANCH_CODE AND RBI2.BANKRBICD=RBI.BANKRBICD " +
                    " LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1017')LNO1 ON LNO1.SRNO=AC.OPR_TYPE " +
                    " LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1016')LNO2 ON LNO2.SRNO=AC.ACC_TYPE " +
                    " LEFT JOIN (SELECT DESCRIPTION,SRNO FROM LOOKUPFORM1 WHERE LNO=1022) LNO ON LNO.SRNO=OW.INSTRU_TYPE WHERE OW.SET_NO='" + setno + "' AND OW.ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND OW.SCROLL_NO='" + scrollno + "' AND OW.BRCD='" + brcd + "' AND OW.STAGE<>'1004' AND (OW.CD='" + DC + "'  or OW.REASON_CODE is null) " +
                    " and  OW.PRDUCT_CODE not in (503,504)";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }

    public int returnout(string stage, string clgflag, string setno, int scrollno, string brcd, string MID, string TxtEntrydate, string TxtProcode, string TxtAccNo, string txtAccTypeid, string txtOpTypeId, string txtpartic, string txtbankcd, string txtbrnchcd, string ddlinsttype, string txtinstdate, string txtinstno, string txtinstamt, string PACMAC, string CLG_GL_NO)
    //public int returnout(string stage)
    {
        try
        {
            //sql = "Update OWG_201607 SET STAGE = '" + stage + "' AND CLG_FLAG='"+clgflag+"' WHERE SET_NO='" + setno + "' AND SCROLL_NO='" + scrollno + "' AND BRCD = '" + brcd + "' AND STAGE = '1002'  AND STAGE <> '1004'";
            result = conn.sExecuteQuery(sql);

            string sqlc = "INSERT INTO OWG_201607 (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + (TxtEntrydate) + "','DD-MM-YYYY'), '01/01/2016','DD-MM-YYYY'),'1','" + TxtProcode + "','" + CLG_GL_NO + "','" + txtAccTypeid + "', '" + txtOpTypeId + "','" + txtpartic + "','" + txtbankcd + "','" + txtbrnchcd + "','" + ddlinsttype + "','" + txtinstdate + "','DD-MM-YYYY'),'" + txtinstno + "','" + txtinstamt + "','1002','" + MID + "','" + PACMAC + "','3', SYSDATE,'DD-MM-YYYY'), 'C'," + setno + ", '" + scrollno + "')";
            string sqld = "INSERT INTO OWG_201607 (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + (TxtEntrydate) + "','DD-MM-YYYY'), '01/01/2016','DD-MM-YYYY'),'1','" + TxtProcode + "','" + TxtAccNo + "','" + txtAccTypeid + "', '" + txtOpTypeId + "','" + txtpartic + "','" + txtbankcd + "','" + txtbrnchcd + "','" + ddlinsttype + "','" + txtinstdate + "','DD-MM-YYYY'),'" + txtinstno + "','" + txtinstamt + "','1002','" + MID + "','" + PACMAC + "','3', SYSDATE,'DD-MM-YYYY'), 'D'," + setno + ", '" + scrollno + "')";
            conn.sExecuteQuery(sqlc);
            conn.sExecuteQuery(sqld);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    public int returnoutauth(string stage, string clgflag, string setno, int scrollno, string brcd, string MID, string TxtEntrydate, string TxtProcode, string TxtAccNo, string txtAccTypeid, string txtOpTypeId, string txtpartic, string txtbankcd, string txtbrnchcd, string ddlinsttype, string txtinstdate, string txtinstno, string txtinstamt, string PACMAC, string CLG_GL_NO, string FL)
    //public int returnout(string stage)
    {
        try
        {
            string tbname, DC,EMD;
            tbname = DC = "";
            string[] TD = TxtEntrydate.Split('/');
            if (FL == "O")
            {
                tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
                DC = "D";
            }
            else if (FL == "I")
            {
                tbname = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
            }
            EMD = EV.GetCK(MID);
            sql = "Update " + tbname + " SET STAGE = '" + stage + "' , CLG_FLAG='" + clgflag + "' , CID = '" + MID + "' WHERE SET_NO='" + setno + "' AND SCROLL_NO='" + scrollno + "' AND BRCD = '" + brcd + "' AND STAGE not in (1004,1003) and ENTRYDATE='" + conn.ConvertDate(TxtEntrydate) + "'";
            result = conn.sExecuteQuery(sql);
            if (result > 0)
            {
                sql = "UPDATE AVSM_" + TD[2].ToString() + TD[1].ToString() + " SET F2='" + EMD + "',STAGE=1003 WHERE ENTRYDATE='" + conn.ConvertDate(TxtEntrydate) + "' AND BRCD='" + brcd + "' AND SETNO='" + setno + "'";
                result = conn.sExecuteQuery(sql);
                if (result > 0)
                {
                    sql = "UPDATE ALLVCR SET STAGE=1003 WHERE ENTRYDATE='" + conn.ConvertDate(TxtEntrydate) + "' AND BRCD='" + brcd + "' AND SETNO='" + setno + "'";
                    result = conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public DataTable ShowImage(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select CustNo From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
            CustNo = Convert.ToDouble(conn.sExecuteScalar(sql));

            if (CustNo.ToString() != "" && CustNo != null && CustNo > 0)
            {
                sql = "Select id, SignName, PhotoName, SignIMG, PhotoImg From Imagerelation Where BRCD = '" + BrCode + "' and CustNo = " + CustNo + " ";
                dt = conn.GetData(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

}