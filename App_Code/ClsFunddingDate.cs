using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;

public class ClsFunddingDate
{
    DbConnection conn = new DbConnection();
    ClsAuthorized AT = new ClsAuthorized();
    string sql = "";
    int Result;
    public ClsFunddingDate()
    {
    }
    public int Getinfotable(GridView Gview, string brcd, string EDT, string FL)
    {
        try
        {
            // conn.ConvertDate(EDT);
            // DateTime dt = DateTime.ParseExact(EDT, "dd/mm/yyyy",System.Globalization.CultureInfo.InvariantCulture);
            // string date = dt.ToString("yyy/mm/dd");  
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

            sql = "select Case when OW.Stage='1003' then 'Funding Required' when OW.Stage='1005' then 'Funded' else 'NA' end as Fund_Status," +
                    "OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, "+
                    " OW.INSTRU_NO InstNo, UM.LOGINCODE maker, OW.PARTICULARS,OW.SET_NO,OW.SCROLL_NO as setscroll ,OW.BANK_CODE,OW.BRANCH_CODE, " +
                    " OW.INSTRU_TYPE,Convert(Varchar(11),OW.INSTRUDATE,103) INSTRUDATE,OW.ACC_TYPE,OW.OPRTN_TYPE" +
                    " from " + tbname + "  OW  " +
                    " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                    " LEFT JOIN USERMASTER UM ON UM.PERMISSIONNO=OW.MID AND UM.BRCD=OW.BRCD " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO" +
                    " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE  " +
                    " WHERE OW.BRCD='" + brcd + "' AND OW.STAGE not in ('1004','1001') AND OW.CLG_FLAG IN ('1','3') and OW.CLG_FLAG<>'4' AND OW.STAGE<>'1004' AND OW.PRDUCT_CODE Not in ('503','504')  AND ENTRYDATE='" + conn.ConvertDate(EDT) + "' order by OW.SET_NO, OW.SCROLL_NO";
            //AND OW.CD='C'
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    //public int InsertData(string MID, string Edate, string BRCD, string FL)
    //{
    //    string[] EDT = Edate.Split('/');
    //    string TNM = "";
    //    try
    //    {
    //        string tbname, DC,PMTMODE;
    //        tbname = DC=PMTMODE = "";
    //        string AC = "";

    //        if (FL == "O")
    //        {
    //            tbname = "owg_" + EDT[2].ToString() + EDT[1].ToString();
    //            PMTMODE = "OC";
    //            AC = "32";
    //        }
    //        else if (FL == "I")
    //        {
    //            tbname = "INWORD_" + EDT[2].ToString() + EDT[1].ToString();
    //            PMTMODE = "IC";
    //            AC = "31";
    //        }
    //        //dateadd(day, datediff(day, 0, OW.ENTRYDATE), 0)
    //        //sql="INSERT INTO AVSM_" + EDT[2].ToString() + EDT[1].ToString()+
    //        //    "(AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE)"+
    //        //     "SELECT 1,OW.ENTRYDATE,OW.ENTRYDATE,OW.ENTRYDATE,GL.GLCODE,OW.PRDUCT_CODE,OW.ACC_NO,OW.PARTICULARS,OW.PARTICULARS,INSTRU_AMOUNT,(cASE WHEN CD='C' THEN '1' WHEN CD='D' THEN '2' END ),(CASE WHEN CD='C' THEN '32' ELSE '31' END),'" + PMTMODE + "',SET_NO,SCROLL_NO,INSTRU_NO,INSTRUDATE,BANK_CODE,BRANCH_CODE,1003,GETDATE(),'" + BRCD + "','" + MID + "',0,0,'" + conn.PCNAME() + "','" + PMTMODE + "',M.CUSTNO,M.CUSTNAME,'000',GETDATE() FROM " + tbname + " OW " +
    //        //       "LEFT JOIN GLMAST GL ON GL.SUBGLCODE=OW.PRDUCT_CODE AND OW.BRCD=GL.BRCD " +
    //        //       "LEFT JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD " +
    //        //       "LEFT JOIN MASTER M ON AC.CUSTNO=M.CUSTNO AND AC.BRCD=M.BRCD " +
    //        //       "WHERE OW.STAGE=1003 AND OW.CLG_FLAG IN (1,3) AND OW.ENTRYDATE='" + conn.ConvertDate(Edate) + "' AND OW.BRCD='" + BRCD + "'";

    //        //Result = conn.sExecuteQuery(sql);
    //        //if (Result > 0)
    //        //{
    //        //    sql="INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) "+
    //        //        "SELECT OW.ENTRYDATE,OW.ENTRYDATE,OW.ENTRYDATE,GL.GLCODE,OW.PRDUCT_CODE,OW.ACC_NO,OW.PARTICULARS,OW.PARTICULARS,(cASE WHEN CD='C' Then INSTRU_AMOUNT else 0 end),(cASE WHEN CD='D' Then INSTRU_AMOUNT else 0 end),(CASE WHEN CD='C' THEN '32' ELSE '31' END),'" + PMTMODE + "',SET_NO,SCROLL_NO,INSTRU_NO,INSTRUDATE,BANK_CODE,BRANCH_CODE,1003,GETDATE(),'" + BRCD + "','" + MID + "',0,0,'" + conn.PCNAME() + "','" + PMTMODE + "',M.CUSTNO,M.CUSTNAME,'000',GETDATE() FROM " + tbname + " OW " + "LEFT JOIN GLMAST GL ON GL.SUBGLCODE=OW.PRDUCT_CODE AND OW.BRCD=GL.BRCD " +
    //        //       "LEFT JOIN AVS_ACC AC ON AC.ACCNO=OW.ACC_NO AND AC.SUBGLCODE=OW.PRDUCT_CODE AND AC.BRCD=OW.BRCD " +
    //        //       "LEFT JOIN MASTER M ON AC.CUSTNO=M.CUSTNO AND AC.BRCD=M.BRCD " +
    //        //       "WHERE OW.STAGE=1003 AND OW.CLG_FLAG IN (1,3) AND OW.ENTRYDATE='" + conn.ConvertDate(Edate) + "' AND OW.BRCD='" + BRCD + "'";

    //        //    Result = conn.sExecuteQuery(sql);
    //         //   if (Result > 0)
    //            //{
    //               //sql = "Update " + tbname + " set STAGE=1005 WHERE STAGE=1003 AND CLG_FLAG IN (1,3) AND ENTRYDATE='" + conn.ConvertDate(Edate) + "' AND BRCD='" + BRCD + "'";
    //               //Result = conn.sExecuteQuery(sql);
    //           // }
    //       // }
    //        //mak 25/01/2017 chnage logic of fundding 
    //        ////DataTable DT = new DataTable();
    //        ////DT = conn.GetDatatable(sql);
    //        ////if (DT.Rows.Count > 0)
    //        ////{
    //        ////    for (int K = 0; K <= DT.Rows.Count - 1; K++)
    //        ////    {

    //        ////        Result = AT.Authorized(DT.Rows[K]["ENTRYDATE"].ToString(), DT.Rows[K]["ENTRYDATE"].ToString(), Edate.ToString(), DT.Rows[K]["GLCODE"].ToString(), DT.Rows[K]["PRDUCT_CODE"].ToString(), DT.Rows[K]["ACC_NO"].ToString(), DT.Rows[K]["PARTICULARS"].ToString(), DT.Rows[K]["PARTICULARS"].ToString(), DT.Rows[K]["INSTRU_AMOUNT"].ToString(), DT.Rows[K]["CD"].ToString() == "D" ? 2.ToString() : 1.ToString(), AC,PMTMODE, DT.Rows[K]["SET_NO"].ToString(), DT.Rows[K]["INSTRU_NO"].ToString(), DT.Rows[K]["INSTRUDATE"].ToString(), DT.Rows[K]["BANK_CODE"].ToString(), DT.Rows[K]["BRANCH_CODE"].ToString(), DT.Rows[K]["STAGE"].ToString(), DateTime.Now.Date.ToString("dd/MM/yyyy"), DT.Rows[K]["BRCD"].ToString(), DT.Rows[K]["MID"].ToString(), DT.Rows[K]["CID"].ToString(), DT.Rows[K]["VID"].ToString(), "CASH", DT.Rows[K]["CUSTNO"].ToString(), DT.Rows[K]["CUSTNAME"].ToString(), "0000");
                    
    //        ////        sql = "Update " + tbname + " set STAGE=1005 WHERE STAGE=1003 AND CLG_FLAG IN (1,3) AND ENTRYDATE='" +conn.ConvertDate(Edate) + "' AND BRCD='" + BRCD + "' AND SET_NO='" + DT.Rows[K]["SET_NO"].ToString() + "'";
    //        ////        Result = conn.sExecuteQuery(sql);
    //        ////    }

                
    //        ////}

    //        sql = "Update " + tbname + " set STAGE=1005 WHERE STAGE=1003 AND CLG_FLAG IN (1,3) AND ENTRYDATE='" + conn.ConvertDate(Edate) + "' AND BRCD='" + BRCD + "'";
    //        Result = conn.sExecuteQuery(sql);
    //        //if (Result > 0)
    //        //{
    //        //    sql="UPDATE AVSM_" + EDT[2].ToString() + EDT[1].ToString() + " SET STAGE=1003 " +
    //        //        " WHERE ENTRYDATE='"+conn.ConvertDate(Edate)+"' AND BRCD='"+BRCD+"' AND 
    //        //}

    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //        //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
    //    }
    //    return Result;
    //}
    public int FundAuthorize(string SETNO,string MID, string Edate, string BRCD, string FL)
    {
        string[] EDT = Edate.Split('/');
        string TNM = "";
        try
        {
            string tbname, DC, PMTMODE;
            tbname = DC = PMTMODE = "";
            string AC = "";

            if (FL == "O")
            {
                tbname = "owg_" + EDT[2].ToString() + EDT[1].ToString();
                PMTMODE = "OC";
                AC = "32";
            }
            else if (FL == "I")
            {
                tbname = "INWORD_" + EDT[2].ToString() + EDT[1].ToString();
                PMTMODE = "IC";
                AC = "31";
            }

            sql = "Update " + tbname + " set STAGE=1005 WHERE STAGE<>1004 AND CLG_FLAG IN (1,3) AND ENTRYDATE='" + conn.ConvertDate(Edate) + "' AND BRCD='" + BRCD + "' and set_no='" + SETNO + "'";
            Result = conn.sExecuteQuery(sql);
            if (FL == "O")
            {
                if (Result > 0)
                {
                    sql = "UPDATE AVSM_" + EDT[2].ToString() + EDT[1].ToString() + " SET STAGE=1003 WHERE ENTRYDATE='" + conn.ConvertDate(Edate) + "' AND BRCD='" + BRCD + "' AND SETNO='" + SETNO + "'";
                    Result = conn.sExecuteQuery(sql);
                    if (Result > 0)
                    {
                        sql = "UPDATE ALLVCR SET STAGE=1003 WHERE ENTRYDATE='" + conn.ConvertDate(Edate) + "' AND BRCD='" + BRCD + "' AND SETNO='" + SETNO + "'";
                        Result = conn.sExecuteQuery(sql);

                        sql = "UPDATE AVS_LNTRX set STAGE=1003 where brcd=" + BRCD + " and entrydate='" + conn.ConvertDate(Edate) + "'and SETNO=" + SETNO + "";
                        int RR= conn.sExecuteQuery(sql);

                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
   
    
}