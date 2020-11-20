using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsOutAuthDo
/// </summary>
public class ClsOutAuthDo
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    ClsEncryptValue EV = new ClsEncryptValue();
    string sql, sqlc, sqld;
    int result;
    string EMD = "";
    public ClsOutAuthDo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetFormData(int setno, int scrollno, int brcd, string EDT, string FL)
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
            sql = "SELECT OW.*,GL.GLNAME PRDNAME,isnull(M.CUSTNAME,GL.GLNAME) CUSTNAME,LNO.DESCRIPTION,GL1.GLNAME ACCTYPE , " +
            " GL2.GLNAME OPETYPE,isnull(RBI.BANK,GL.GLNAME) BANK,isnull(RBI2.BRANCH,GL.GLNAME) BRANCH,isnull(LNO1.DESCRIPTION,'NA') OPRTYPE,isnull(LNO2.DESCRIPTION,'NA') ACCTYPEA " +
            " FROM " + tbname + " OW " +
                    " LEFT JOIN GLMAST GL ON GL.SUBGLCODE=OW.PRDUCT_CODE AND GL.BRCD=OW.BRCD " +
                    " LEFT JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD  " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO" +
                    " LEFT JOIN GLMAST GL1 ON GL1.SUBGLCODE=OW.ACC_TYPE AND GL1.BRCD=OW.BRCD  " +
                    " LEFT JOIN GLMAST GL2 ON GL2.SUBGLCODE=OW.OPRTN_TYPE AND GL2.BRCD=OW.BRCD " +
                    " LEFT JOIN (SELECT DESCR BANK,BANKRBICD FROM RBIBANK WHERE BRANCHRBICD='0' AND STATECD ='400') RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                    " LEFT JOIN (SELECT DESCR BRANCH,BRANCHRBICD,BANKRBICD FROM RBIBANK WHERE STATECD ='400') RBI2 ON RBI2.BRANCHRBICD=OW.BRANCH_CODE AND RBI2.BANKRBICD=RBI.BANKRBICD " +
                    " LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1017')LNO1 ON LNO1.SRNO=AC.OPR_TYPE " +
                    " LEFT JOIN (select DESCRIPTION , SRNO from lookupform1 where LNO='1016')LNO2 ON LNO2.SRNO=AC.ACC_TYPE " +
                    " LEFT JOIN (SELECT DESCRIPTION,SRNO FROM LOOKUPFORM1 WHERE LNO=1022) LNO ON LNO.SRNO=OW.INSTRU_TYPE WHERE OW.SET_NO='" + setno + "' AND OW.SCROLL_NO='" + scrollno + "' AND OW.ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND OW.BRCD='" + brcd + "' AND OW.STAGE not in ('1003','1004','1005') and OW.PRDUCT_CODE not in (504,503)";
            //AND OW.CD='C'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }


    public int Authorize(int stage, int setno, int scrollno, int brcd, string MID,string EDT,string FL)
    {
        try
        {
            string tbname, DC,EMD;
            string IWStage = "";
            tbname = DC = "";
            string[] TD = EDT.Split('/');
            if (FL == "O")
            {
                tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
                DC = "D";
                IWStage = "1003";
            }
            else if (FL == "I")
            {
                tbname = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
                IWStage = "1005"; // Bcz Funding not required for inward Abhishek 2017-07-22

            }

            EMD = EV.GetCK(MID);

            if (FL == "O")
            {
                sql = "Update " + tbname + " SET STAGE = '" + stage + "', CID='" + MID + "' WHERE SET_NO='" + setno + "' AND ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND SCROLL_NO='" + scrollno + "' AND BRCD = '" + brcd + "'  AND STAGE <> '1004'";
                result = conn.sExecuteQuery(sql);
            }
            else if (FL == "I")
            {
                sql = "Update " + tbname + " SET STAGE = '" + IWStage + "', CID='" + MID + "' WHERE SET_NO='" + setno + "' AND ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND SCROLL_NO='" + scrollno + "' AND BRCD = '" + brcd + "'  AND STAGE <> '1004'";
                result = conn.sExecuteQuery(sql);
                if (result > 0)
                {
                    sql = "UPDATE AVSM_" + TD[2].ToString() + TD[1].ToString() + " SET F2='" + EMD + "',STAGE=1003 WHERE ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND BRCD='" + brcd + "' AND SETNO='" + setno + "'";
                    result = conn.sExecuteQuery(sql);
                    if (result > 0)
                    {
                        sql = "UPDATE ALLVCR SET STAGE=1003 WHERE ENTRYDATE='" + conn.ConvertDate(EDT) + "' AND BRCD='" + brcd + "' AND SETNO='" + setno + "'";
                        result = conn.sExecuteQuery(sql);

                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }
    public int cancel(int stage, int setno, int scrollno, int brcd, string MID,string EDT)
    {
        try
        {
            string tbname,tbnamem, DC,EMD;
            tbname = DC = "";
            string[] TD = EDT.Split('/');
            
            tbname = "owg_" + TD[2].ToString() + TD[1].ToString();
            DC = "D";
            tbnamem = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            EMD = EV.GetVK(MID);

            sql = "Update " + tbname + " SET STAGE = '" + stage + "',VID='" + MID + "' WHERE ENTRYDATE='" + conn.ConvertDate(EDT) + "' and  SET_NO='" + setno + "' AND SCROLL_NO='" + scrollno + "' AND BRCD = '" + brcd + "' AND STAGE <> '1003'  AND STAGE <> '1004'";
            result = conn.sExecuteQuery(sql);
            if (result > 0)
            {
                sql = "Update AVSM_" + tbnamem + " SET F3='" + EMD + "' ,STAGE = '" + stage + "',VID='" + MID + "' WHERE ENTRYDATE='" + conn.ConvertDate(EDT) + "' and  SET_NO='" + setno + "'  AND BRCD = '" + brcd + "' AND STAGE <> '1003'  AND STAGE <> '1004'";
                result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    public DataTable GetSetDetails(string EDT, string SETNO)
    {
        try
        {
            sql = "Exec Isp_Clearing_Operations @Flag='OWGMUL',@SFlag='',@Edt='" + conn.ConvertDate(EDT) + "',@Setno='" + SETNO + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int InsertOwMultiple(string FL,string BRCD,string EDT,string CUSTNO,string CUSTNAME,string GLCODE,string SUBGL,string ACCNO,string P1,string P2,string AMT,string TRX,string CLGFL,string INSTTYPE,string INSTBKCD,string INSTBRCD,string INSTNO,string INSTDATE,string MID)
    {
        try
        {
            string EMD = EV.GetMK(MID);
            sql = "Exec Isp_Clearing_Operations @F1='" + EMD + "',@Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "',@Cutsno='" + CUSTNO + "',@Custname='" + CUSTNAME + "',@Glcode='" + GLCODE + "',@Subglcode='" + SUBGL + "',@Accno='" + ACCNO + "',@P1='" + P1 + "',@P2='" + P2 + "',@Amount='" + AMT + "',@TrxType='" + TRX + "',@Clg_Fl='" + CLGFL + "',@InstType='" + INSTTYPE + "',@InstBankcd='" + INSTBKCD + "',@InstBrCd='" + INSTBRCD + "',@InstNo='" + INSTNO + "',@InstDate='" + conn.ConvertDate(INSTDATE) + "',@Mid='" + MID + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable GetCRDR(string FL,string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "Exec Isp_Clearing_Operations @Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "',@Brcd='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    //Exec Isp_Clearing_Operations @Flag='BINDMUL',@SFlag='',@Edt='2017-06-08',@MID='4',@Brcd='4'
    public int GetMultipleGrid(GridView GD, string FL, string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "Exec Isp_Clearing_Operations @Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "',@Brcd='" + BRCD + "'";
            result = conn.sBindGrid(GD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
        //Exec Isp_Clearing_Operations @Flag='POST',@SFlag='',@Edt='2017-06-08',@MID='4',@Brcd='4'

    public string PostOw_Multiple(string FL, string EDT, string MID, string BRCD)
    {
        try
        {
            EMD=EV.GetMK(MID);
            sql="Exec Isp_Clearing_Operations @Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@MID='" + MID + "',@Brcd='" + BRCD + "',@F1='"+EMD+"'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public int Delete_Multiple(string FL,string BRCD,string Iden,string EDT)
    {
        try
        {

            sql = "Exec Isp_Clearing_Operations @Flag='" + FL + "',@IDEN='" + Iden + "',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}
