using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsInClear
/// </summary>
public class ClsInwordClear
{

    string sql, sqlc, sqld;
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    ClsEncryptValue EV = new ClsEncryptValue();
    int RES = 0;
    public ClsInwordClear()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Get_CLG_GL_NO(string brcd)
    {
        int CLG_GL_NO = 0;
        try
        {
            string sql_clg_gl = "select CLG_GL_NO from INWORD_PARAMETER WHERE BRCD='" + brcd + "' AND STAGE <>  1004";
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
            string sql_clg_gl = "select RETURN_GL_NO from INWORD_PARAMETER WHERE BRCD='" + brcd + "' AND STAGE <>  1004";
            CLG_GL_NO = Convert.ToInt32(conn.sExecuteScalar(sql_clg_gl));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return CLG_GL_NO;
    }

    public int GetNewSetNo(string brcd)
    {
        int SetNo = 1;
        try
        {

            sql = "(select (ISNULL(MAX(SET_NO),0) +TO_NUMBER('1')) FROM INWORD_2016010 WHERE BRCD = '" + brcd + "')";
            SetNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return SetNo;
    }

    public int GetNewScrollNo(int setno, string brcd)
    {
        int ScrollNo = 0;
        try
        {
            sql = "select (ISNULL(MAX(SCROLL_NO),0) +TO_NUMBER('1')) FROM INWORD_2016010 WHERE BRCD = '" + brcd + "' AND SET_NO='" + setno + "'";
            ScrollNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return ScrollNo;
    }


    public int GetcurrentSetNo(string brcd)
    {
        int SetNo = 1;
        try
        {
            sql = "(select MAX(SET_NO) FROM INWORD_2016010 WHERE BRCD = '" + brcd + "')";
            SetNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return SetNo;
    }

    public int InsertNewSetNo(string GLCODE,string Entrydate, string BRCD, string Procode, string AccNo, string AccTypeid, string OpTypeId, string partic, string bankcd, string brnchcd, string insttype, string instdate, string instno, string instamt, string MID, string PACMAC, int SetNo, int ScrollNo, int CLG_GL_NO, string ENTP,string STAGE,string RFID)
    {
        int result = 0;
        try
        {

            string[] TD = Entrydate.Split('/');
            string Custno = BD.GetCustno(AccNo, BRCD, Procode);
            string Custname = BD.GetCustName(Custno, BRCD);
            string CONSBGL = "", TBNAME = "",EMD="";

            TBNAME = "INWORD_" + TD[2].ToString() + TD[1].ToString();
            CONSBGL = "503";
            if (Custno == null || Custno == "")
            {
                Custno = "0";
                Custname = "BANK";
            }
            EMD = EV.GetMK(MID);
            sqld = "SELECT ISNULL(MAX(SCROLL_NO),0)+1 FROM INWORD_" + TD[2].ToString() + TD[1].ToString() + " WHERE ENTRYDATE='" + conn.ConvertDate(Entrydate) + "' AND SET_NO='" + SetNo + "' AND BRCD='" + BRCD + "'";
            ScrollNo = Convert.ToInt32(conn.sExecuteScalar(sqld));

            if (ENTP == "D")
            {

                sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                       " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + Procode + "','" + AccNo + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                result = conn.sExecuteQuery(sqlc);
                if (result > 0)
                {
                    sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                          " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','503','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                }
                if (result > 0)//Abhishek allvcr and avsm
                {

                    sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                             "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                            "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                        result = conn.sExecuteQuery(sqlc);
                    }
                    sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                         "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                        result = conn.sExecuteQuery(sqlc);
                    }
                }

            }
            else if (ENTP == "C")
            {
                sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','1','" + CLG_GL_NO + "','" + CLG_GL_NO + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                result = conn.sExecuteQuery(sqlc);

                if (result > 0)
                {
                    sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                          " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','503','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','1', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                }
                if (result > 0)//Abhishek allvcr and avsm
                {

                    sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                             "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                            "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";
                        result = conn.sExecuteQuery(sqlc);
                    }
                    sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                         "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                    result = conn.sExecuteQuery(sqlc);
                    if (result > 0)
                    {
                        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(F1,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + EMD + "',1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + RFID + "',GETDATE())";

                        result = conn.sExecuteQuery(sqlc);
                    }
                }

            }
        }
        //result = conn.sExecuteQuery(sqld);

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }


    public int InsertUnPass(string GLCODE, string Entrydate, string BRCD, string Procode, string AccNo, string AccTypeid, string OpTypeId, string partic, string bankcd, string brnchcd, string insttype, string instdate, string instno, string instamt, string MID, string PACMAC, int SetNo, int ScrollNo, int CLG_GL_NO, string ENTP, string STAGE)
    {
        int result = 0;
        try
        {

            string[] TD = Entrydate.Split('/');
            string Custno = BD.GetCustno(AccNo, BRCD, Procode);
            string Custname = BD.GetCustName(Custno, BRCD);
            string CONSBGL = "", TBNAME = "",EMD;

            TBNAME = "INWORD_" + TD[2].ToString() + TD[1].ToString();
            CONSBGL = "503";


            EMD = EV.GetMK(MID);

            sqld = "SELECT ISNULL(MAX(SCROLL_NO),0)+1 FROM INWORD_" + TD[2].ToString() + TD[1].ToString() + " WHERE ENTRYDATE='" + conn.ConvertDate(Entrydate) + "' AND SET_NO='" + SetNo + "' AND BRCD='" + BRCD + "'";
            ScrollNo = Convert.ToInt32(conn.sExecuteScalar(sqld));

            if (ENTP == "D")
            {

                sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                       " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','" + Procode + "','" + AccNo + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','4', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                result = conn.sExecuteQuery(sqlc);
                if (result > 0)
                {
                    sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                          " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','503','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','4', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                }
                //if (result > 0)//Abhishek allvcr and avsm
                //{

                //    sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //             "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";
                //    result = conn.sExecuteQuery(sqlc);
                //    if (result > 0)
                //    {
                //        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //            "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";
                //        result = conn.sExecuteQuery(sqlc);
                //    }
                //    sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //         "VALUES(1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";

                //    result = conn.sExecuteQuery(sqlc);
                //    if (result > 0)
                //    {
                //        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //        "VALUES(1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";

                //        result = conn.sExecuteQuery(sqlc);
                //    }
                //}

            }
            else if (ENTP == "C")
            {
                sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','1','" + CLG_GL_NO + "','" + CLG_GL_NO + "','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','4', getdate(), 'C','" + SetNo + "', '" + ScrollNo + "')";
                result = conn.sExecuteQuery(sqlc);

                if (result > 0)
                {
                    sqlc = "INSERT INTO INWORD_" + TD[2].ToString() + TD[1].ToString() + " (ENTRYDATE, FUNDING_DATE, BRCD, PRDUCT_CODE, ACC_NO, ACC_TYPE, OPRTN_TYPE, PARTICULARS, BANK_CODE, BRANCH_CODE, INSTRU_TYPE, INSTRUDATE, INSTRU_NO, INSTRU_AMOUNT, STAGE, MID, CID, VID, PAC_MAC, CLG_FLAG, SYSTEM_DATE, CD, SET_NO, SCROLL_NO ) " +
                          " VALUES ('" + conn.ConvertDate(Entrydate) + "','" + conn.ConvertDate(Entrydate) + "','" + BRCD + "','503','0','" + AccTypeid + "', '" + OpTypeId + "','" + partic + "','" + bankcd + "','" + brnchcd + "','" + insttype + "','" + conn.ConvertDate(instdate) + "','" + instno + "','" + instamt + "','" + STAGE + "','" + MID + "','0','0','" + PACMAC + "','4', getdate(), 'D','" + SetNo + "', '" + ScrollNo + "')";
                    result = conn.sExecuteQuery(sqlc);
                }
                //if (result > 0)//Abhishek allvcr and avsm
                //{

                //    sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //             "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "0" + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";
                //    result = conn.sExecuteQuery(sqlc);
                //    if (result > 0)
                //    {
                //        sqlc = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //            "VALUES('" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + "0" + "','" + instamt + "','" + "31" + "','" + "IC" + "','" + SetNo + "','" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";
                //        result = conn.sExecuteQuery(sqlc);
                //    }
                //    sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //         "VALUES(1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + GLCODE + "','" + Procode + "','" + AccNo + "','" + partic + "','" + partic + "','" + instamt + "','" + "1" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";

                //    result = conn.sExecuteQuery(sqlc);
                //    if (result > 0)
                //    {
                //        sqlc = "INSERT INTO AVSM_" + TD[2].ToString() + TD[1].ToString() + "(AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                //        "VALUES(1,'" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + conn.ConvertDate(Entrydate.Replace("12:00:00", "")) + "','" + CONSBGL + "','" + CONSBGL + "','0','" + partic + "','" + partic + "','" + instamt + "','" + "2" + "','" + "31" + "','" + "IC" + "','" + SetNo + "', '" + ScrollNo + "','" + instno + "','" + conn.ConvertDate(instdate.Replace("12:00:00", "")) + "','" + bankcd + "','" + brnchcd + "','" + "1001" + "',getdate(), '" + BRCD + "','" + MID + "','" + "0" + "','" + "0" + "','" + conn.PCNAME().ToString() + "','" + "IC" + "','" + Custno + "','" + Custname + "','" + "" + "',GETDATE())";

                //        result = conn.sExecuteQuery(sqlc);
                //    }
                //}

            }
        }
        

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }


    public double GetAmount(string BRCD, string EDate, string FL)
    {
        double AMT = 0;
        try
        {
            string TBNAme, DC,Extra="";
            TBNAme = DC = "";
            string[] TD = EDate.Split('/');
            if (FL == "IN")
            {
                TBNAme = "INWORD_";
                DC = "D";
                Extra = "and CLG_FLAG not in ('4')";
            }
            else if (FL == "OUT")
            {
                TBNAme = "OWG_";
                DC = "C";
                Extra = "and CLG_FLAG not in ('4')";
            }
            else if (FL == "IWRT")
            {
                TBNAme = "INWORD_";
                DC = "C";
                Extra = "and CLG_FLAG=3";
            }
            else if (FL == "OWRT")
            {
                TBNAme = "OWG_";
                DC = "C";    
                Extra = "and CLG_FLAG=3";
            }
            sql = "select ABS(ISNULL(SUM(INSTRU_AMOUNT),0)) FROM " + TBNAme + TD[2].ToString() + TD[1].ToString() + " WHERE  ENTRYDATE='" + conn.ConvertDate(EDate) + "' AND ACC_NO<>0 and STAGE<>'1004' and CD='" + DC + "' AND BRCD='" + BRCD + "' " + Extra + "";
            AMT = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AMT;
    }


    public double GetDifference(string BRCD, string EDate, string FL)
    {
        double AMT = 0;
        try
        {
            string TBNAme, DC, Extra = "";
            TBNAme = DC = "";
            string[] TD = EDate.Split('/');
            if (FL == "IN")
            {
                TBNAme = "INWORD_" +TD[2].ToString() + TD[1].ToString() ;
                DC = "D";
                Extra = "and CLG_FLAG<>'4'";
            }
            else if (FL == "OUT")
            {
                TBNAme = "OWG_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
                Extra = "and CLG_FLAG<>'4'";
            }
            else if (FL == "IWRT")
            {
                TBNAme = "INWORD_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
                Extra = "and CLG_FLAG=3";
            }
            else if (FL == "OWRT")
            {
                TBNAme = "OWG_" + TD[2].ToString() + TD[1].ToString();
                DC = "C";
                Extra = "and CLG_FLAG=3";
            }
            //sql = "select ISNULL(SUM(INSTRU_AMOUNT),0) FROM " + TBNAme + TD[2].ToString() + TD[1].ToString() + " WHERE  ENTRYDATE='" + conn.ConvertDate(EDate) + "' AND CD='" + DC + "' AND BRCD='" + BRCD + "' " + Extra + "";
            sql = "select abs((Select isnull(Sum(INSTRU_AMOUNT),0) From " + TBNAme + " " +
                    " Where ENTRYDATE='" + conn.ConvertDate(EDate) + "'  And PRDUCT_CODE not in ('504',503) And CD='C' " +
                    " " + Extra + " and stage<>'1004') " +
                    " - " +
                    " (Select isnull(Sum(INSTRU_AMOUNT),0) From " + TBNAme + "" +
                    " Where ENTRYDATE='" + conn.ConvertDate(EDate) + "' And PRDUCT_CODE not in ('504',503) And CD='D' " +
                    " " + Extra + " and stage<>'1004')) ";
            AMT = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AMT;
    }
   
    // Delete Entries
    public int DeleteOwgClearingEntry(int setno, string brcd)
    {
        int result = 0;
        try
        {
            sql = "UPDATE INWORD_2016010 SET STAGE='1004' WHERE SET_NO='" + setno + "' AND BRCD = '" + brcd + "' AND STAGE <> '1003' ";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }


    public void DeleteData(string BRCD)
    {
        try
        {
            string sqltruncate = "delete from INWORD_201610_TEMP where BRCD='" + BRCD + "'";
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

    public void ReportData(string sbrcd)
    {

        DataTable dt = new DataTable();
        try
        {
            sql = "select OW.SET_NO, OW.SCROLL_NO SCRL, OW.PRDUCT_CODE AT,OW.ACC_NO AC, M.CUSTNAME Name, OW.INSTRU_AMOUNT Amount,RBI.DESCR BankName, " +
                    " OW.INSTRU_NO InstNo, OW.INSTRUDATE Date1, OW.PARTICULARS from OWG_201607 OW " +
                    " LEFT JOIN AVS_ACC ACC ON ACC.ACCNO=OW.ACC_NO AND ACC.BRCD = OW.BRCD AND OW.PRDUCT_CODE=ACC.SUBGLCODE " +
                    " LEFT JOIN MASTER M ON M.CUSTNO=ACC.CUSTNO " +
                    " LEFT JOIN (SELECT DESCR,BANKRBICD FROM RBIBANK WHERE  BRANCHRBICD=0 AND STATECD=400 )RBI ON RBI.BANKRBICD=OW.BANK_CODE " +
                    " WHERE OW.BRCD='" + sbrcd + "' AND OW.STAGE <>'1004' AND OW.CD='C' AND TO_CHAR(OW.SYSTEM_DATE), 'DD-MM-YYYY') = TO_CHAR(SYSDATE), 'DD-MM-YYYY') order by OW.SET_NO, SCRL ";
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
                    string sqlinsert = "Insert into INWORD_2016010_TEMP VALUES ('" + dt.Rows[j][0] + "','" + dt.Rows[j][1] + "','" + dt.Rows[j][2] + "','" + dt.Rows[j][3] + "','" + dt.Rows[j][4] + "','" + dt.Rows[j][5] + "','" + dt.Rows[j][6] + "','" + dt.Rows[j][7] + "','" + Convert.ToDateTime(dt.Rows[j][8]).ToString("dd/MM/yyyy") + "','DD-MM-YYYY'),'" + dt.Rows[j][9] + "')";
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

    public DataTable GetUnpass(string BRCD, string Setno, string Scroll, string EDT,string FL)
    {
        try
        {
            string[] TD=EDT.Split('/');
            string TBNAME="";
            if (FL=="I")
            {
                TBNAME="INWORD_" +TD[2].ToString() + TD[1].ToString();
            }
            else if(FL=="O")
            {
                TBNAME="OWG_" +TD[2].ToString() + TD[1].ToString();
            }

            sql = "select * from " + TBNAME + " where ENTRYDATE='" + conn.ConvertDate(EDT) + "' and BRCD='" + BRCD + "' and set_no='" + Setno + "' and PRDUCT_CODE not in(503,504)";
            DT= conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int DeleteUnpass(string BRCD, string SETNO,string EDT,string FL)
    {
        int Res=0;
        try
        {
             string[] TD=EDT.Split('/');
            string TBNAME="";
            if (FL=="I")
            {
                TBNAME="INWORD_" +TD[2].ToString() + TD[1].ToString();
            }
            else if(FL=="O")
            {
                TBNAME="OWG_" +TD[2].ToString() + TD[1].ToString();
            }
            sql = "UPDATE " + TBNAME + " SET STAGE=1004 WHERE sET_NO=" + SETNO + " AND BRCD='" + BRCD + "' AND ENTRYDATE='" + conn.ConvertDate(EDT) + "'";
            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public string GetClgFlag(string BRCD, string SETNO, string EDT, string FL)
    {
        int Res = 0;
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
            sql = "SELECT CLG_FLAG FROM "+TBNAME+" WHERE ENTRYDATE='"+conn.ConvertDate(EDT)+"' and BRCD='"+BRCD+"' AND SET_NO='"+SETNO+"'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetLoanLimit(string BRCD,string SUBGL,string ACCNo)
    {
        try
        {
            sql = "select isnull(LIMIT,0) from LOANINFO where LOANGLCODE='" + SUBGL + "' and CUSTACCNO='" + ACCNo + "' and BRCD='" + BRCD + "' AND LMSTATUS=1 ORDER BY DUEDATE DESC";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string InstDateDiff(string EDT, string INSTDT)
    {
        try
        {
            sql = "select DATEDIFF(DD,'"+conn.ConvertDate(INSTDT)+"','"+conn.ConvertDate(EDT)+"')";
            sql = conn.sExecuteScalar(sql);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string InstMonthDiff(string EDT, string INSTDT)
    {
        try
        {
           // sql = "select DATEDIFF(MM,'" + conn.ConvertDate(INSTDT) + "','" + conn.ConvertDate(EDT) + "')";
            sql = "select dbo.UF_MonthDifference('" + conn.ConvertDate(INSTDT) + "','" + conn.ConvertDate(EDT) + "')";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetLoanCatDuedate(string BRCD,string SBGL,string ACCNO,string FL)
    {
        try
        {
            if (FL == "DC")
            {
                sql = "SELECT TOP 1 CONVERT(VARCHAR(12),LI.DUEDATE,103)+'_'+CONVERT(VARCHAR(10),LG.LOANCATEGORY) FROM LOANINFO LI " +
                    " INNER JOIN LOANGL LG ON LI.LOANGLCODE=LG.LOANGLCODE AND LI.BRCD=LG.BRCD " +
                    " WHERE LI.LOANGLCODE='" + SBGL + "' AND LI.CUSTACCNO='" + ACCNO + "' AND LI.BRCD='" + BRCD + "' AND LI.LMSTATUS=1  " +
                    " ORDER BY LI.DUEDATE DESC ";
            }
            else if (FL == "C")
            {
                sql = "SELECT LOANCATEGORY FROM LOANGL WHERE BRCD='" + BRCD + "' AND LOANGLCODE='" + SBGL + "'";
            }
            SBGL = conn.sExecuteScalar(sql);
        }
        catch (Exception eX)
        {
            ExceptionLogging.SendErrorToText(eX);
        }
        return SBGL;
    }


}