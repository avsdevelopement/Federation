using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

public class ClsOutClear
{
    string sql, sqlc, sqld;
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsEncryptValue Ev = new ClsEncryptValue();
    string EMDDel = "";
    int Result;
    public ClsOutClear()
    {

    }

    public int Get_CLG_GL_NO(string brcd)
    {
        int CLG_GL_NO = 0;
        try
        {
            string sql_clg_gl = "select CLG_GL_NO from OWG_PARAMETER WHERE BRCD='" + brcd + "' AND STAGE <>  1004";
            CLG_GL_NO = Convert.ToInt32(conn.sExecuteScalar(sql_clg_gl));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return CLG_GL_NO;
    }

    public int Get_CLG_GL_NO_return(string brcd)
    {
        int CLG_GL_NO = 0;
        try
        {
            string sql_clg_gl = "select RETURN_GL_NO from OWG_PARAMETER WHERE BRCD='" + brcd + "' AND STAGE <>  1004";
            CLG_GL_NO = Convert.ToInt32(conn.sExecuteScalar(sql_clg_gl));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return CLG_GL_NO;
    }

    public int GetNewSetNo(string brcd, string EDT)
    {
        int SetNo = 1;
        try
        {
            string[] TD = EDT.Split('/');
            sql = "(select (ISNULL(MAX(SET_NO),0) +TO_NUMBER('1')) FROM OWG_" + TD[2].ToString() + TD[1].ToString() + " WHERE BRCD = '" + brcd + "')";
            SetNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return SetNo;
    }

    public int GetNewScrollNo(int setno, string brcd, string EDT)
    {
        int ScrollNo = 0;
        try
        {
            string[] TD = EDT.Split('/');
            sql = "select (ISNULL(MAX(SCROLL_NO),0) +TO_NUMBER('1')) FROM OWG_" + TD[2].ToString() + TD[1].ToString() + " WHERE BRCD = '" + brcd + "' AND SET_NO='" + setno + "'";
            ScrollNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return ScrollNo;
    }


    public int GetcurrentSetNo(string brcd, string EDT)
    {
        int SetNo = 1;
        try
        {
            string[] TD = EDT.Split('/');
            sql = "(select MAX(SET_NO) FROM OWG_" + TD[2].ToString() + TD[1].ToString() + " WHERE BRCD = '" + brcd + "')";
            SetNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return SetNo;
    }

    public int InsertNewSetNo(string GLCODE,string Entrydate, string BRCD, string Procode, string AccNo, string AccTypeid, string OpTypeId, string partic, string bankcd, string brnchcd, string insttype, string instdate, string instno, string instamt, string MID, string PACMAC, int SetNo, int ScrollNo,string CLG_GL_NO ,string  FL,string SBFLG,string STAGE,string RFID)
    {
        int result = 0;
        try
        {
            string TBNAME = "", CONSBGL = "", ACT = "", EMD = "";
            string[] TD = Entrydate.Split('/');
            string Custno = BD.GetCustno(AccNo, BRCD, Procode);
            string Custname = BD.GetCustName(Custno, BRCD);
            if (Custno == null || Custname == null)
            {
                Custno = "0";
                Custname="N/A";
            }
            if (SBFLG == "I")
            {
                TBNAME = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                CONSBGL = "503";
                ACT = "31";
            }

            else if (SBFLG == "O")
            {
                TBNAME = "OWG_" + TD[2].ToString() + TD[1].ToString();
                CONSBGL = "504";
                ACT = "32";
            }
            EMD = Ev.GetMK(MID);

            if(FL=="C")
            {
                sqlc = "INSERT INTO " + TBNAME + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + Procode + "','" + AccNo + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO "+TBNAME+" (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                              " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + CONSBGL + "','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                        result = conn.sExecuteQuery(sqlc);
                    }

                    if (result > 0)//Abhishek allvcr and avsm
                    {
                       
                        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                                 "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + ACT + "','" + "OC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                        result = conn.sExecuteQuery(sqlc);
                        if (result > 0)
                        {
                            sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                                "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + ACT + "','" + "OC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                            result = conn.sExecuteQuery(sqlc);
                        }
                        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                             "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + ACT + "','" + "OC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                        result = conn.sExecuteQuery(sqlc);
                        if (result > 0)
                        {
                            sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                            "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + ACT + "','" + "OC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                            result = conn.sExecuteQuery(sqlc);
                        }

                          
                        
                    }
            }
            else if (FL == "D")
            {
                sqlc = "INSERT INTO " + TBNAME + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + Procode + "','" + AccNo + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                result = conn.sExecuteQuery(sqlc);
                if (result > 0)
                {
                    sqlc = "INSERT INTO " + TBNAME + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                          " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + CONSBGL + "','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                }

                if (result > 0)
                {
                    sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                             "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + ACT + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                            "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + ACT + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                        result = conn.sExecuteQuery(sqlc);
                    }
                    

                    sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + ACT + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + ACT + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                        result = conn.sExecuteQuery(sqlc);
                    }
                }
            }
            //sqld = "INSERT INTO OWG_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','1','" + CLG_GL_NO + "','" + CLG_GL_NO + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','1001','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
          
            //result = conn.sExecuteQuery(sqld);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }


    public int InsertNewSetNoReturn(string GLCODE, string Entrydate, string BRCD, string Procode, string AccNo, string AccTypeid, string OpTypeId, string partic, string bankcd, string brnchcd, string insttype, string instdate, string instno, string instamt, string MID, string PACMAC, int SetNo, int ScrollNo, string CLG_GL_NO, string FL, string SBFLG, string STAGE,string RFID)
    {
        int result = 0;
        try
        {
            string Custno = BD.GetCustno(AccNo, BRCD, Procode);
            string Custname = BD.GetCustName(Custno, BRCD);
            string TBNAME = "", CONSBGL = "", ACT = "" ,EMD="";
            string[] TD = Entrydate.Split('/');
            if (Custno == null || Custno == "")
            {
                Custname = "N/A";
                Custno = "0";
            }
            if (SBFLG == "I")
            {
                TBNAME = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                CONSBGL = "503";
                ACT = "31";
               
                
            }

            else if (SBFLG == "O")
            {
                TBNAME = "OWG_" + TD[2].ToString() + TD[1].ToString();
                CONSBGL = "504";
                ACT = "32";
            }

            EMD = Ev.GetMK(MID);
            if (FL == "C")
            {
                sqlc = "INSERT INTO " + TBNAME + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + Procode + "','" + AccNo + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','3', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                result = conn.sExecuteQuery(sqlc);
                if (result > 0)
                {
                    sqlc = "INSERT INTO " + TBNAME + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                          " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + CONSBGL + "','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','3', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                }
                if (result > 0)
                {
                    sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                             "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + ACT + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                            "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + ACT + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                        result = conn.sExecuteQuery(sqlc);
                    }


                    sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + ACT + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + ACT + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                        result = conn.sExecuteQuery(sqlc);
                    }
                }

            }
            else if (FL == "D")
            {
                sqlc = "INSERT INTO " + TBNAME + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + Procode + "','" + AccNo + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','3', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                result = conn.sExecuteQuery(sqlc);
                if (result > 0)
                {
                    sqlc = "INSERT INTO " + TBNAME + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                          " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + CONSBGL + "','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','3', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                }

                if (result > 0)//Abhishek allvcr and avsm
                {

                    sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                             "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + ACT + "','" + "OC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                            "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + ACT + "','" + "OC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                        result = conn.sExecuteQuery(sqlc);
                    }
                    sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                         "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + ACT + "','" + "OC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + ACT + "','" + "OC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "OC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                        result = conn.sExecuteQuery(sqlc);
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

    // Delete Entries
    public int DeleteOwgClearingEntry(int setno, string brcd, string EDT)
    {
        int result = 0;
        try
        {
            string[] TD = EDT.Split('/');
            sql = "UPDATE OWG_" + TD[2].ToString() + TD[1].ToString() + " SET STAGE='1004' WHERE SET_NO='" + setno + "' AND BRCD = '" + brcd + "' AND STAGE <> '1003' ";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }


    public void DeleteData(string EDT,string BRCD) //BRCD ADDED --Abhishek
    {
        try
        {
            string[] TD = EDT.Split('/');
            string sqltruncate = "delete from OWG_201607_TEMP where BRCD='" + BRCD + "'";
            string sqltruncatecommit = "commit";
            conn.sExecuteQuery(sqltruncate);
            conn.sExecuteQuery(sqltruncatecommit);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }

    public void ReportData(string sbrcd, string EDT)
    {

        DataTable dt = new DataTable();
        try
        {
            string[] TD = EDT.Split('/');

            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
                        " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS from OWG_" + TD[2].ToString() + TD[1].ToString() + " OW " +
                        " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                        " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                        " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                        " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <>'1004' AND OW.CD='C' AND OW.EntryDate = '" + conn.ConvertDate(EDT) + "' order by OW.SET_NO, SCRL ";
            dt = conn.GetDatatable(sql);

            int j = 0;
            if (dt.Rows.Count > 1)
            {

            }
            int i = 0;
            if (dt.Rows.Count > 1)
            {
                i = dt.Rows.Count;
                while (i > 0)
                {
                    string sqlinsert = "Insert into OWG_" + TD[2].ToString() + TD[1].ToString() + "_TEMP VALUES ('" + dt.Rows[j][0] + "','" + dt.Rows[j][1] + "','" + dt.Rows[j][2] + "','" + dt.Rows[j][3] + "','" + dt.Rows[j][4] + "','" + dt.Rows[j][5] + "','" + dt.Rows[j][6] + "','" + dt.Rows[j][7] + "','" + Convert.ToDateTime(dt.Rows[j][8]).ToString("dd/MM/yyyy") + "','DD-MM-YYYY'),'" + dt.Rows[j][9] + "')";
                    conn.sExecuteQuery(sqlinsert);
                    i = i - 1;
                    j = j + 1;
                }
            }


            int k = 0;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
    }
    public int BindClearing(GridView Gview, string smid, string sbrcd, string EDT)
    {
        try
        {
            string[] TD = EDT.Split('/');
            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
                    " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS,(CASE WHEN OW.STAGE='1001' THEN 'CREATED'  when OW.STAGE='1002' THEN 'MODIFIED' WHEN OW.STAGE='1003' THEN 'AUTHORIZE' WHEN OW.STAGE=1005 THEN 'FUNDED' ELSE '' END) STAGE, " +
                    " (CASE WHEN OW.CLG_FLAG='1' THEN 'PASS' WHEN CLG_FLAG='4' THEN 'UNPASS' END) STATUS from OWG_" + TD[2].ToString() + TD[1].ToString() + " OW " +
                    " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                    " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                    " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <> '1004' AND OW.CLG_FLAG ='1'  AND OW.CD='C' AND OW.EntryDate ='" + conn.ConvertDate(EDT) + "' and OW.PRDUCT_CODE NOT IN(503,504) and ow.INSTRU_NO<>0 order by OW.SET_NO, OW.SCROLL_NO ";
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
    public int UdateValues(string Setno,string Parti, string AMT, string BRCD,string MID, string BankCD, string BranchCD,string INSTNO,string INSTDT,string Edate,string FL)
    {
        try
        {
            string[] TD = Edate.Split('/');
            string Tbname = "", TBNAME2 = "AVSM_";
            if(FL=="I")
            {
                Tbname="INWORD_";
                
            }
            else  if(FL=="O")
            {
                Tbname="OWG_";
            }

            sql = "update " + Tbname + TD[2].ToString() + TD[1].ToString() + " set STAGE='1002',VID='" + MID + "',PARTICULARS='" + Parti + "',INSTRU_NO='" + INSTNO + "',INSTRUDATE='" + conn.ConvertDate(INSTDT) + "',INSTRU_AMOUNT='" + AMT + "',BRANCH_CODE='" + BranchCD + "',BANK_CODE='" + BankCD + "' where ENTRYDATE='" + conn.ConvertDate(Edate) + "' and SET_NO='" + Setno + "' and BRCD='" + BRCD + "'";
            Result = conn.sExecuteQuery(sql);
            if (Result > 0)
            {
                sql = "update " + TBNAME2 + TD[2].ToString() + TD[1].ToString() + " set STAGE='1002',VID='" + MID + "',PARTICULARS='" + Parti + "',INSTRUMENTNO='" + INSTNO + "',INSTRUMENTDATE='" + conn.ConvertDate(INSTDT) + "',AMOUNT='" + AMT + "',INSTBRCD='" + BranchCD + "',INSTBANKCD='" + BankCD + "' where ENTRYDATE='" + conn.ConvertDate(Edate) + "' and SETNO='" + Setno + "' and BRCD='" + BRCD + "' ";
                Result = conn.sExecuteQuery(sql);
            
              //if (Result > 0)
              //  {
              //      sql = "update ALLVCR set STAGE='1002',PARTICULARS='" + Parti + "',INSTRUMENTNO='" + INSTNO + "',INSTRUMENTDATE='" + conn.ConvertDate(INSTDT) + "',CREDIT='" + AMT + "',INSTBRCD='" + BranchCD + "',INSTBANKCD='" + BankCD + "' where ENTRYDATE='" + conn.ConvertDate(Edate) + "' and SETNO='" + Setno + "' and BRCD='" + BRCD + "' and Debit='0' ";
              //      Result = conn.sExecuteQuery(sql);
              //      sql = "update ALLVCR set STAGE='1002',PARTICULARS='" + Parti + "',INSTRUMENTNO='" + INSTNO + "',INSTRUMENTDATE='" + conn.ConvertDate(INSTDT) + "',DEBIT='" + AMT + "',INSTBRCD='" + BranchCD + "',INSTBANKCD='" + BankCD + "' where ENTRYDATE='" + conn.ConvertDate(Edate) + "' and SETNO='" + Setno + "' and BRCD='" + BRCD + "' and Credit='0' ";
              //      Result = conn.sExecuteQuery(sql);
              //  }
            }
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int DeleteValues(string Setno, string Edate, string BRCD,string FL,string SFL,string MID)
    {
        try
        {
            string[] TD = Edate.Split('/');
            string Tbname = "", TBNAME2 = "AVSM_";
            if (FL == "I")
            {
                Tbname = "INWORD_";

            }
            else if (FL == "O")
            {
                Tbname = "OWG_";
            }

            EMDDel = Ev.GetVK(MID);
            sql = "update " + Tbname + TD[2].ToString() + TD[1].ToString() + " set STAGE=1004 where ENTRYDATE='" + conn.ConvertDate(Edate) + "' and SET_NO='" + Setno + "' and BRCD='" + BRCD + "'";
            Result = conn.sExecuteQuery(sql);
            if (Result > 0 && SFL=="PASS")
            {
                sql = "update " + TBNAME2 + TD[2].ToString() + TD[1].ToString() + " set F3='" + EMDDel + "',STAGE=1004 where ENTRYDATE='" + conn.ConvertDate(Edate) + "' and SETNO='" + Setno + "' and BRCD='" + BRCD + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "update ALLVCR set STAGE=1004 where ENTRYDATE='" + conn.ConvertDate(Edate) + "' and SETNO='" + Setno + "' and BRCD='" + BRCD + "' ";
                    Result = conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string GetOutContra(string brcd)
    {
        sql = "";
        sql = conn.sExecuteScalar(sql);
        return sql;
    }
}