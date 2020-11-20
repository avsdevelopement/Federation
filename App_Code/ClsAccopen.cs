using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;

public class ClsAccopen
{
    DbConnection conn = new DbConnection();
    DataTable dt1 = new DataTable();
    string sql = "", AccNo = "", sql1 = "", sql2 = "", sResult = "", sResult2 = "";
    int Result = 0, Res = 0, nom1, nom2, joint;

    public ClsAccopen()
    {
    }

    public string CheckCustStatus(string BrCode, string CustNo)
    {
        try
        {
            sql = "Select Stage From Master Where CustNo = '" + CustNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
  


    public string GetFontType(string BrCode)
    {
        try
        {
            sql = "select isnull(Listvalue,'E') as FontType from PARAMETER where LISTFIELD='ChqPrint_EM' and BRCD='" + BrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            sResult = "E";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }


    public string GetCustName(string BRCD, string CustNo)
    {
        try
        {
            if (GetFontType(BrCode: BRCD).ToString().Equals("M"))
            {
                sql = "SELECT REFERENCENAME2 FROM MASTER WHERE CUSTNO=" + CustNo;
            }
            else if (GetFontType(BrCode: BRCD).ToString().Equals("E"))
            {
                sql = "SELECT  CUSTNAME FROM  MASTER WHERE  CUSTNO=" + CustNo;
            }
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            sResult = "E";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string SeparateDateFormat(string data)
    {
        string returnString = "";
        string replacedString = data;
       // string replacedString = Regex.Replace(data, @"/", "-");
        char[] fromcharacters = new char[100];
        fromcharacters = replacedString.ToCharArray();
        int toLength = (replacedString.Length);
        char[] toCharacters = new char[toLength*4];
        int j = 0;
        try
        {

            for (int i = 0; i < toLength; i++)
            {                
                toCharacters[j] = fromcharacters[i];               
                toCharacters[++j] = ' ';
                toCharacters[++j] = ' ';
                ++j;
 
            }
        
            returnString = new string(toCharacters);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return returnString;
    }


    public string GetChqPrintDate(string BRCD)
    {
        try
        {
            sql = "select isnull(LISTVALUE,'03/09/2018') from PARAMETER where LISTFIELD='ChqPrintDate' and BRCD='" + BRCD + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            sResult = "E";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int insert(string BRCD, string GLCODE, string SUBGLCODE, string CUSTNO, string OPENINGDATE, string MID, string ACC_TYPE, string OPR_TYPE, string MINOR_ACC, string M_CUSTNO, string M_OPRNAME, string M_RELATION, string M_DOB, string FCHKB, string FSMS, string FRDC, string FESTS, string FAD, string FIB, string FMB, string PERIOD, string AMT, string Stage, string Shr_BrCd, string RefCustNo, string AgentNo, string RecSend)
    {
        try
        {
            string Acc = "";

            sql = "select LISTVALUE from parameter where LISTFIELD='LOANACCNO'";
            Acc = conn.sExecuteScalar(sql);

            if (SUBGLCODE == "4" && Acc == "Y")
            {
                //string Shr = "";
                //sql = "select LISTVALUE from parameter where LISTFIELD='SHRALLOT'";
                //Shr = conn.sExecuteScalar(sql);
                //if (Shr == "HO")
                //    sql = "select CustNO from avs_acc where brcd=1 and subglcode=4 and stage<>1003 and ACC_STATUS<>3 ";
                //else
                // sql = "select CustNO from avs_acc where brcd='"+BRCD+"' and subglcode=4 and stage<>1003 and ACC_STATUS<>3 ";
                sql = "select " + CUSTNO + "";
            }

            else
            {
                if (GLCODE == "2")
                {
                    sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' and glcode='" + GLCODE + "'";
                }
                else
                {
                    sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' ";
                }
            }

            string ACCNO = conn.sExecuteScalar(sql);
            sql = "select accno from avs_acc  where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "' and glcode='" + GLCODE + "' and custno='" + CUSTNO + "' and acc_status<>3 and stage<>1004 and accno='" + ACCNO + "'";
            string Flag = conn.sExecuteScalar(sql);

            if (Flag == "" || Flag == null)
            {
                //sql = "INSERT INTO AVS_ACC( BRCD,GLCODE,SUBGLCODE,CUSTNO,ACCNO,OPENINGDATE, ACC_STATUS,STAGE,MID,PCMAC,SYSTEMDATE,ACC_TYPE,OPR_TYPE,MINOR_ACC,M_CUSTNO,M_OPRNAME,M_RELATION,M_DOB,FCHKB,FSMS,FRDC,FESTS,FAD,FIB,FMB,D_PERIOD,D_AMOUNT, SHR_BR,Ref_custNo,Ref_Agent,RecSend) " +
                //    "VALUES('" + BRCD + "', '" + GLCODE + "', '" + SUBGLCODE + "', '" + CUSTNO + "', '" + ACCNO + "', '" + conn.ConvertDate(OPENINGDATE).ToString() + "', '1', '" + Stage + "', '" + MID + "','" + conn.PCNAME() + "',GETDATE(),'" + ACC_TYPE + "','" + OPR_TYPE + "','" + MINOR_ACC + "','" + M_CUSTNO + "','" + M_OPRNAME + "','" + M_RELATION + "','" + M_DOB + "','" + FCHKB + "','" + FSMS + "','" + FRDC + "','" + FESTS + "','" + FAD + "','" + FIB + "','" + FMB + "','" + PERIOD + "','" + AMT + "', '" + Shr_BrCd + "','" + RefCustNo + "','" + AgentNo + "','" + RecSend + "')";

                sql = "INSERT INTO AVS_ACC( BRCD,GLCODE,SUBGLCODE,CUSTNO,ACCNO,OPENINGDATE, ACC_STATUS,STAGE,MID,PCMAC,SYSTEMDATE,ACC_TYPE,OPR_TYPE,MINOR_ACC,M_CUSTNO,M_OPRNAME,M_RELATION,M_DOB,FCHKB,FSMS,FRDC,FESTS,FAD,FIB,FMB,D_PERIOD,D_AMOUNT, SHR_BR,Ref_custNo,Ref_Agent,RecSend) " +
                   "VALUES('" + BRCD + "', '" + GLCODE + "', '" + SUBGLCODE + "', '" + CUSTNO + "', '" + ACCNO + "', '" + conn.ConvertDate(OPENINGDATE).ToString() + "', '1', '" + Stage + "', '" + MID + "','" + conn.PCNAME() + "',GETDATE(),'" + ACC_TYPE + "','" + OPR_TYPE + "','" + MINOR_ACC + "','" + M_CUSTNO + "','" + M_OPRNAME + "','" + M_RELATION + "','" + M_DOB + "','" + FCHKB + "','" + FSMS + "','" + FRDC + "','" + FESTS + "','" + FAD + "','" + FIB + "','" + FMB + "','" + PERIOD + "','" + AMT + "', '" + Shr_BrCd + "','" + RefCustNo + "','" + AgentNo + "',case when '" + RecSend + "'='' then null else '" + RecSend + "' end )";

                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    int ac = Convert.ToInt32(ACCNO);
                    if (GLCODE == "2")
                    {
                        sql = "update glmast set lastno='" + ac + "' where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "' and glcode='" + GLCODE + "'";
                    }
                    else
                    {
                        sql = "update glmast set lastno='" + ac + "' where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "'";
                    }
                    int RC = conn.sExecuteQuery(sql);

                }
            }
            Result = Convert.ToInt32(ACCNO);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    //public int insert(string BRCD, string GLCODE, string SUBGLCODE, string CUSTNO, string OPENINGDATE, string MID, string ACC_TYPE, string OPR_TYPE, string MINOR_ACC, string M_CUSTNO, string M_OPRNAME, string M_RELATION, string M_DOB, string FCHKB, string FSMS, string FRDC, string FESTS, string FAD, string FIB, string FMB, string PERIOD, string AMT, string Stage, string Shr_BrCd, string RefCustNo, string AgentNo)
    //{
    //    try
    //    {
    //        string Acc = "";

    //        sql = "select LISTVALUE from parameter where LISTFIELD='LOANACCNO'";
    //        Acc = conn.sExecuteScalar(sql);

    //        if (SUBGLCODE == "4" && Acc == "Y")
    //        {
    //            //string Shr = "";
    //            //sql = "select LISTVALUE from parameter where LISTFIELD='SHRALLOT'";
    //            //Shr = conn.sExecuteScalar(sql);
    //            //if (Shr == "HO")
    //            //    sql = "select CustNO from avs_acc where brcd=1 and subglcode=4 and stage<>1003 and ACC_STATUS<>3 ";
    //            //else
    //               // sql = "select CustNO from avs_acc where brcd='"+BRCD+"' and subglcode=4 and stage<>1003 and ACC_STATUS<>3 ";
    //            sql = "select "+CUSTNO+"";
    //        }

    //        else
    //        {
    //            if (GLCODE == "2")
    //            {
    //                sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' and glcode='" + GLCODE + "'";
    //            }
    //            else  
    //            {
    //                sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' ";
    //            }
    //        }

    //        string ACCNO = conn.sExecuteScalar(sql);
    //        sql = "select accno from avs_acc  where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "' and glcode='" + GLCODE + "' and custno='" + CUSTNO + "' and acc_status<>3 and stage<>1004 and accno='" + ACCNO + "'";
    //        string Flag = conn.sExecuteScalar(sql);

    //        if (Flag == "" || Flag == null)
    //        {
    //            sql = "INSERT INTO AVS_ACC( BRCD,GLCODE,SUBGLCODE,CUSTNO,ACCNO,OPENINGDATE, ACC_STATUS,STAGE,MID,PCMAC,SYSTEMDATE,ACC_TYPE,OPR_TYPE,MINOR_ACC,M_CUSTNO,M_OPRNAME,M_RELATION,M_DOB,FCHKB,FSMS,FRDC,FESTS,FAD,FIB,FMB,D_PERIOD,D_AMOUNT, SHR_BR,Ref_custNo,Ref_Agent) " +
    //                "VALUES('" + BRCD + "', '" + GLCODE + "', '" + SUBGLCODE + "', '" + CUSTNO + "', '" + ACCNO + "', '" + conn.ConvertDate(OPENINGDATE).ToString() + "', '1', '" + Stage + "', '" + MID + "','" + conn.PCNAME() + "',GETDATE(),'" + ACC_TYPE + "','" + OPR_TYPE + "','" + MINOR_ACC + "','" + M_CUSTNO + "','" + M_OPRNAME + "','" + M_RELATION + "','" + M_DOB + "','" + FCHKB + "','" + FSMS + "','" + FRDC + "','" + FESTS + "','" + FAD + "','" + FIB + "','" + FMB + "','" + PERIOD + "','" + AMT + "', '" + Shr_BrCd + "','" + RefCustNo + "','" + AgentNo + "')";

    //            Result = conn.sExecuteQuery(sql);

    //            if (Result > 0)
    //            {
    //                int ac = Convert.ToInt32(ACCNO);
    //                if (GLCODE == "2")
    //                {
    //                    sql = "update glmast set lastno='" + ac + "' where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "' and glcode='" + GLCODE + "'";
    //                }
    //                else
    //                {
    //                    sql = "update glmast set lastno='" + ac + "' where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "'";
    //                }
    //                int RC = conn.sExecuteQuery(sql);

    //            }
    //        }
    //        Result = Convert.ToInt32(ACCNO);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;
    //}


    public int insertDDSPIGMY(string BRCD, string GLCODE, string SUBGLCODE, string CUSTNO, string OPENINGDATE, string DUE, string MID, string ACC_TYPE, string OPR_TYPE, string PERIOD, string AMT, string ROI, string M_AMT, string HDN, string txtOpenAccNo)
    {
        try
        {
            if (HDN == "Y")//Dhanya Shetty//28/12/2017//As per darade  sir if Listfield (Parameter)DDSACCYN='Y' then existing code 
            {
                if (GLCODE == "2")
                {
                    sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' and glcode='" + GLCODE + "'";
                }
                else
                {
                    sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' ";
                }

                string ACCNO = conn.sExecuteScalar(sql);
                sql = "EXEC SP_DDSPIGMY @FLAG='AD',@BRCD='" + BRCD + "',@GLCODE='" + GLCODE + "',@SUBGLCODE='" + SUBGLCODE + "',@CUSTNO='" + CUSTNO + "',@ACCNO='" + ACCNO + "',@OPENINGDATE='" + conn.ConvertDate(OPENINGDATE).ToString() + "',@DUEDATE='" + conn.ConvertDate(DUE).ToString() + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@ACC_TYPE='" + ACC_TYPE + "',@OPR_TYPE='" + OPR_TYPE + "',@D_PERIOD='" + PERIOD + "',@D_AMOUNT='" + AMT + "',@RATEOFINT='" + ROI + "',@MATURITYAMT='" + M_AMT + "'";//SP ADDED BY ANKITA 26/04/2017
                //sql = "INSERT INTO AVS_ACC( BRCD,GLCODE,SUBGLCODE,CUSTNO,ACCNO,OPENINGDATE,CLOSINGDATE,ACC_STATUS,STAGE,MID,PCMAC,SYSTEMDATE,ACC_TYPE,OPR_TYPE,MINOR_ACC,M_CUSTNO,M_OPRNAME,M_RELATION,M_DOB,FCHKB,FSMS,FRDC,FESTS,FAD,FIB,FMB,D_PERIOD,D_AMOUNT) " +
                //    "VALUES('" + BRCD + "','" + GLCODE + "','" + SUBGLCODE + "','" + CUSTNO + "','" + ACCNO + "','" + conn.ConvertDate(OPENINGDATE).ToString() + "',' ','1','1001','" + MID + "','" + conn.PCNAME() + "',GETDATE(),'" + ACC_TYPE + "','" + OPR_TYPE + "','" + MINOR_ACC + "','" + M_CUSTNO + "','" + M_OPRNAME + "','" + M_RELATION + "','" + M_DOB + "','" + FCHKB + "','" + FSMS + "','" + FRDC + "','" + FESTS + "','" + FAD + "','" + FIB + "','" + FMB + "','" + PERIOD + "','" + AMT + "')";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    int ac = Convert.ToInt32(ACCNO);
                    if (GLCODE == "2")
                    {
                        sql = "update glmast set lastno='" + ac + "' where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "' and glcode='" + GLCODE + "'";
                    }
                    else
                    {
                        sql = "update glmast set lastno='" + ac + "' where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "'";
                    }
                    int RC = conn.sExecuteQuery(sql);

                }
                Result = Convert.ToInt32(ACCNO);
            }
            else if (HDN == "N")//if Listfield (Parameter) DDSACCYN='N" then this code/09/01/2018
            {
                if (GLCODE == "2")
                {
                    sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from avs1000 WHERE type='DDSACCYN' and  BRCD='" + BRCD + "'";
                }
                string DACCNO = conn.sExecuteScalar(sql);
                sql = "EXEC SP_DDSPIGMY @FLAG='AD',@BRCD='" + BRCD + "',@GLCODE='" + GLCODE + "',@SUBGLCODE='" + SUBGLCODE + "',@CUSTNO='" + CUSTNO + "',@ACCNO='" + txtOpenAccNo + "',@OPENINGDATE='" + conn.ConvertDate(OPENINGDATE).ToString() + "',@DUEDATE='" + conn.ConvertDate(DUE).ToString() + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@ACC_TYPE='" + ACC_TYPE + "',@OPR_TYPE='" + OPR_TYPE + "',@D_PERIOD='" + PERIOD + "',@D_AMOUNT='" + AMT + "',@RATEOFINT='" + ROI + "',@MATURITYAMT='" + M_AMT + "'";//SP ADDED BY ANKITA 26/04/2017
                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {
                    //int ac = Convert.ToInt32(DACCNO);
                    //if (GLCODE == "2")
                    //{
                    //    sql = "update avs1000 set lastno='" + ac + "' where type='DDSACCYN' and brcd='" + BRCD + "'  "+
                    //     " update glmast set lastno='" + ac + "' where brcd='" + BRCD + "' and subglcode='" + SUBGLCODE + "'";
                    //}
                    //int RC = conn.sExecuteQuery(sql);
                }
                Result = Convert.ToInt32(txtOpenAccNo);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string OpenAccNo(string BRCD, string GLCODE, string SUBGLCODE)//Dhanya Shetty
    {
        try
        {
            if (GLCODE == "2")
            {
                sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' and glcode='" + GLCODE + "'";
            }
            else
            {
                sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from glmast WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' ";
            }
            AccNo = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AccNo;
    }
    public string Getcustname(string custno)//ankita 22/11/2017 brcd removed
    {
        try
        {
            sql = "SELECT (CUSTNAME+'_'+(Convert(varchar(10),Convert(bigint,CUSTNO)))) CUSTNAME FROM MASTER WHERE CustNo = '" + custno + "' And Stage In ('1001', '1002', '1003')";
            custno = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }

    public string GetOpenAccNo(string BrCode, string GlCode, string SubGlCode)
    {
        try
        {
            sql = "Select MAX(ISNULL(A.LastNo, 0)) + 1 As LastNo From (Select ISNULL(LastNo, 0) As LastNo From GlMast Where BrCd = '" + BrCode + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + SubGlCode + "')A";
            AccNo = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AccNo;
    }

    public DataTable GetInfo(string custno, string BRCD, string accno, string AT)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select * from avs_acc where brcd='" + BRCD + "'  and accno='" + accno + "' AND GLCODE='" + custno + "' and subglcode='" + AT + "' and stage In ('1001','1002','1003')";

            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAgentInfo(string BrCode, string AgentNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select GLNAME,LASTNO,GLCODE from GLMAST where BRCD='" + BrCode + "' And GLCODE='2' AND SUBGLCODE='" + AgentNo + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }


    public string Getacctype(string acctype, string BRCD)
    {
        try
        {
            sql = "SELECT (MAX(LASTNO)+1)||'-'||GLCODE||'-'||GLNAME FROM GLMAST WHERE SUBGLCODE ='" + acctype + "' AND BRCD='" + BRCD + "' GROUP BY GLCODE,GLNAME";
            acctype = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return acctype;
    }

    public string Getmodeofopr(string mofopr)
    {
        try
        {
            sql = "select DESCRIPTION from lookupform1 where LNO='1017' and srno='" + mofopr + "'";
            mofopr = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return mofopr;
    }
    public int update(string txtcstno, string txtaccno, string txtodate, string PT, string txttype, string txtmopr, string MACC, string txtcustid, string txtacconam, string Relation, string txtmdate, string FCHK, string FSMS, string FRDC, string FESTS, string FAD, string FIB, string FMB, string BRCD, string RefCustNo, string AgentNo, string EDate, string Mid, string RecSend)
    {
        try
        {

            sql1 = "Exec SP_ACCHISTORY    @FLAG='HIS_MOD_ACCOPEN',@DEPOSITACCNO='" + txtaccno + "',@DEPOSITGLCODE='" + PT + "',@BRCD='" + BRCD + "',@EDate='" + conn.ConvertDate(EDate) + "',@MID='" + Mid + "'";//DHANYA SHETTY//
            Result = conn.sExecuteQuery(sql1);
            if (Result > 0)
            {

                string sql = "UPDATE avs_acc set openingdate='" + conn.ConvertDate(txtodate).ToString() + "',acc_type='" + txttype + "',opr_type='" + txtmopr + "', minor_acc='" + MACC + "',m_custno='" + txtcustid + "',m_oprname='" + txtacconam + "',m_relation='" + Relation + "',m_dob='" + txtmdate + "',fchkb='" + FCHK + "',fsms='" + FSMS + "',frdc='" + FRDC + "',fests='" + FESTS + "',fad='" + FAD + "',fib='" + FIB + "', fmb='" + FMB + "',Ref_custNo='" + RefCustNo + "',Ref_Agent='" + AgentNo + "' ,RecSend='" + RecSend + "',STAGE='1002' where CUSTNO ='" + txtcstno + "' and subglcode='" + PT + "' and accno='" + txtaccno + "' AND STAGE<>'1004'  and ACC_STATUS <>'3' ";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public int delete(string brcd, string pcode, string custno, string accno, string GLCODE, string EDate, string Mid)
    {
        try
        {
            sql2 = "Exec SP_ACCHISTORY    @FLAG='HIS_DEL_ACCOPEN',@DEPOSITACCNO='" + accno + "',@DEPOSITGLCODE='" + pcode + "',@BRCD='" + brcd + "',@EDate='" + conn.ConvertDate(EDate) + "',@MID='" + Mid + "'";//DHANYA SHETTY//
            Result = conn.sExecuteQuery(sql2);
            //sql = "UPDATE Avs_Acc SET STAGE='1004' WHERE BRCD = '" + brcd + "' AND SUBGLCODE = '" + pcode + "' AND CUSTNO = '" + custno + "' AND ACCNO='" + accno + "' AND STAGE <> 1003";
            if (Result > 0)
            {
                sql = "delete from Avs_Acc  WHERE BRCD = '" + brcd + "' AND SUBGLCODE = '" + pcode + "' AND CUSTNO = '" + custno + "' AND ACCNO='" + accno + "' AND STAGE <> 1003"; //Dhanya Shetty//
                Result = conn.sExecuteQuery(sql);
                sql1 = "update Glmast set LASTNO=(select max(accno) from Avs_Acc Where Brcd='" + brcd + "'  And Subglcode='" + pcode + "') Where Brcd='" + brcd + "' And Subglcode='" + pcode + "'  ";
                Res = conn.sExecuteQuery(sql1);
                return Result;
                //if (GLCODE == "2")
                //{
                //  }
                //else
                //{
                //    sql2 = "UPDATE Avs_Acc SET STAGE='1004' WHERE BRCD = '" + brcd + "' AND SUBGLCODE = '" + pcode + "' AND CUSTNO = '" + custno + "' AND ACCNO='" + accno + "' AND STAGE <> 1003";
                //    Result = conn.sExecuteQuery(sql2);
                //    sql1 = "update Glmast set LASTNO=(select (max(accno)-1) from Avs_Acc Where Brcd='" + brcd + "'  And Subglcode='" + pcode + "') Where Brcd='" + brcd + "' And Subglcode='" + pcode + "'  ";
                //    Res = conn.sExecuteQuery(sql1);
                //    return Result;
                //}
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }


    public int authorize(string brcd, string pcode, string custno, string accno, string mid)
    {
        try
        {
            sql = "UPDATE AVS_ACC SET STAGE = '1003' WHERE BRCD = '" + brcd + "' AND SUBGLCODE = '" + pcode + "' AND CUSTNO = '" + custno + "' AND ACCNO='" + accno + "' AND MID <> '" + mid + "' AND STAGE <> 1004";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetStage(string BRCD, string Custno, string AC, string AT)
    {
        try
        {
            sql = "SELECT STAGE FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND CUSTNO='" + Custno + "' and accno='" + AC + "' and Subglcode='" + AT + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public DataTable GetDDSAccno(string AN, string brcd)
    {
        try
        {
            string sql1 = "select GLNAME,(CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from GLMAST where SUBGLCODE='" + AN + "' AND GLCODE='2'  AND BRCD='" + brcd + "'";
            dt1 = conn.GetDatatable(sql1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt1;
    }

    public DataTable GetDDSACC_Details(string BRCD, string SUBGL, string ACCNO, string GLCODE, string MID)
    {
        try
        {
            sql = "EXEC SP_DDSPIGMY @FLAG='SELECT',@BRCD='" + BRCD + "',@GLCODE='" + GLCODE + "',@SUBGLCODE='" + SUBGL + "',@ACCNO='" + ACCNO + "',@MID='" + MID + "'";//SP ADDED BY ANKITA 26/04/2017
            //sql = "SELECT * FROM AVS_ACC WHERE SUBGLCODE='" + SUBGL + "' AND GLCODE='" + GLCODE + "' AND ACCNO='" + ACCNO + "' AND BRCD='" + BRCD + "'"; //--BRCD Added --Abhishek
            dt1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt1;
    }

    public int GetDepCnt(string brcd, string CustNo)
    {
        try
        {
            sql = "SELECT COUNT(*) FROM DEPOSITINFO WHERE BRCD = '" + brcd + "' AND CUSTNO = '" + CustNo + "' AND LMSTATUS = '1'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }

    public DataTable GetOpeningFormDetails(string BRCD, string CTNO, string SUBGL, string GLCODE)
    {
        try
        {
            sql = "EXEC SP_OPENINGFORM @BRCD='" + BRCD + "', @CUSTNO='" + CTNO + "',@SUBGLCODE='" + SUBGL + "',@GLCODE='" + GLCODE + "' ";
            dt1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt1;
    }

    public int GETDATA(string brcd, string SUBGLCODE, string ACCNO)
    {
        try
        {
            sql = " select COUNT(*) from depositinfo_history where brcd='" + brcd + "' and depositglcode='" + SUBGLCODE + "' and custaccno='" + ACCNO + "' AND stage<>'1004'";//added stage by ankita on 21/07/2017
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }
    public DataTable GETAVS_ACCDATA(string BRCD, string SUBGL, string ACCNO, string GLCODE)
    {
        try
        {
            sql = "EXEC SP_DDSMODIFY @BRCD='" + BRCD + "',@SUBGLCODE='" + SUBGL + "',@ACCNO='" + ACCNO + "',@D_AMOUNT='',@PERIOD='',@FLAG='SELECT'";
            dt1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt1;
    }
    public string GETDATEDIFF(string DATE1, string DATE2)
    {
        sql = "select DATEDIFF(dd,'" + conn.ConvertDate(DATE1) + "','" + conn.ConvertDate(DATE2) + "')";
        string RESULT = conn.sExecuteScalar(sql);
        return RESULT;

    }


    public int nomiinsert1(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string NomName1, string Relation1, string DOB1, string SUBGLCODE, string CUSTNO, string ACCNO)
    {//Dhanya shetty//21-03-2017
        try
        {
            sql = "INSERT INTO NOMINEEDETAILS(BRCD,GLCODE,MID,EFFECTDATE,STAGE,SRNO,NOMINEENAME,RELATION,DOB,SUBGLCODE,CUSTNO,ACCNO) VALUES"
                + "('" + BRCD + "','" + GLCODE + "','" + MID + "','" + conn.ConvertDate(EDate).ToString() + "','" + Stage + "','" + Srno + "','" + NomName1 + "','" + Relation1 + "','" + conn.ConvertDate(DOB1).ToString() + "','" + SUBGLCODE + "','" + CUSTNO + "','" + ACCNO + "')";
            nom1 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom1;
    }

    public int nomiinsert2(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string NomName2, string Relation2, string DOB2, string SUBGLCODE, string CUSTNO, string ACCNO)
    {//Dhanya shetty//21-03-2017
        try
        {
            sql = "INSERT INTO NOMINEEDETAILS(BRCD,GLCODE,MID,EFFECTDATE,STAGE,SRNO,NOMINEENAME,RELATION,DOB,SUBGLCODE,CUSTNO,ACCNO) VALUES "
                + "('" + BRCD + "','" + GLCODE + "','" + MID + "','" + conn.ConvertDate(EDate).ToString() + "','" + Stage + "','" + Srno + "','" + NomName2 + "','" + Relation2 + "','" + conn.ConvertDate(DOB2).ToString() + "','" + SUBGLCODE + "','" + CUSTNO + "','" + ACCNO + "')";
            nom2 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom2;
    }
    public int nomiupdate1(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string NomName1, string Relation1, string DOB1, string SUBGLCODE, string CUSTNO, string ACCNO)
    {//Dhanya shetty//21-03-2017
        try
        {
            sql = "update NOMINEEDETAILS set stage=1002 ,NOMINEENAME='" + NomName1 + "',RELATION=  '" + Relation1 + "',DOB = '" + conn.ConvertDate(DOB1).ToString() + "' where BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "' and CUSTNO='" + CUSTNO + "' and SRNO='" + Srno + "' and  ACCNO='" + ACCNO + "' AND STAGE<>'1004'   ";
            nom1 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom1;

    }
    public int nomiupdate2(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string NomName2, string Relation2, string DOB2, string SUBGLCODE, string CUSTNO, string ACCNO)
    {//Dhanya shetty//21-03-2017
        try
        {
            sql = "update NOMINEEDETAILS set stage=1002 ,NOMINEENAME='" + NomName2 + "',RELATION=  '" + Relation2 + "',DOB = '" + conn.ConvertDate(DOB2).ToString() + "' where BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and SRNO='" + Srno + "' and ACCNO='" + ACCNO + "' AND STAGE<>'1004'   ";
            nom2 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom2;

    }
    public int nomidelete1(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "delete from NOMINEEDETAILS  WHERE BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SUBGLCODE + "' AND CUSTNO = '" + CUSTNO + "' and SRNO='" + Srno + "'  AND ACCNO='" + ACCNO + "' AND stage not in(1003,1004)";
            //sql = "Update NOMINEEDETAILS Set STAGE = '1004' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and SRNO='" + Srno + "' and ACCNO='" + ACCNO + "'  AND STAGE not in(1003,1004)   ";
            nom1 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom1;
    }
    public int nomidelete2(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "delete from NOMINEEDETAILS  WHERE BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SUBGLCODE + "' AND CUSTNO = '" + CUSTNO + "' and SRNO='" + Srno + "'  AND ACCNO='" + ACCNO + "' AND stage  not in(1003,1004)";
            //sql = "Update NOMINEEDETAILS Set STAGE = '1004' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and SRNO='" + Srno + "' and ACCNO='" + ACCNO + "'  AND STAGE not in(1003,1004)   ";
            nom2 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom2;
    }
    public int nomiautho1(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO, string mid)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "Update NOMINEEDETAILS Set STAGE = '1003' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and SRNO='" + Srno + "' and ACCNO='" + ACCNO + "' AND MID <> '" + mid + "' AND STAGE<>'1004'   ";
            nom1 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom1;
    }
    public int nomiautho2(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO, string mid)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "Update NOMINEEDETAILS Set STAGE = '1003' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and SRNO='" + Srno + "' and ACCNO='" + ACCNO + "' AND MID <> '" + mid + "'  AND STAGE<>'1004'   ";
            nom2 = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return nom2;
    }//Dhanya shetty//21-03-2017
    public int jointinsert1(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string Jname1, string Relation1, string DOB1, string SUBGLCODE, string CUSTNO, string ACCNO, string JOINTCUSTNO)
    {
        try
        {
            sql = "INSERT INTO JOINT(BRCD,GLCODE,MID,EFFECTDATE,STAGE,JOINTSRNO,JOINTNAME,JOINTRELATION,JOINTDOB,SUBGLCODE,CUSTNO,ACCNO,JOINTCUSTNO)values('" + BRCD + "','" + GLCODE + "','" + MID + "'," +
                "'" + conn.ConvertDate(EDate).ToString() + "','" + Stage + "','" + Srno + "','" + Jname1 + "','" + Relation1 + "','" + conn.ConvertDate(DOB1).ToString() + "','" + SUBGLCODE + "','" + CUSTNO + "','" + ACCNO + "','" + JOINTCUSTNO + "')";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;
    }//Dhanya shetty//21-03-2017
    public int jointinsert2(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string Jname2, string Relation2, string DOB2, string SUBGLCODE, string CUSTNO, string ACCNO, string JOINTCUSTNO)
    {
        try
        {
            sql = "INSERT INTO JOINT(BRCD,GLCODE,MID,EFFECTDATE,STAGE,JOINTSRNO,JOINTNAME,JOINTRELATION,JOINTDOB,SUBGLCODE,CUSTNO,ACCNO,JOINTCUSTNO)values('" + BRCD + "','" + GLCODE + "','" + MID + "'," +
                "'" + conn.ConvertDate(EDate).ToString() + "','" + Stage + "','" + Srno + "','" + Jname2 + "','" + Relation2 + "','" + conn.ConvertDate(DOB2).ToString() + "','" + SUBGLCODE + "','" + CUSTNO + "','" + ACCNO + "','" + JOINTCUSTNO + "')";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;
    }//Dhanya shetty//21-03-2017
    public int jointupdate1(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string Jname1, string Relation1, string DOB1, string SUBGLCODE, string CUSTNO, string ACCNO)
    {
        try
        {
            sql = "update JOINT set stage=1002, JOINTNAME='" + Jname1 + "',JOINTRELATION=  '" + Relation1 + "',JOINTDOB=  '" + conn.ConvertDate(DOB1).ToString() + "' where BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and JOINTSRNO='" + Srno + "' and  ACCNO='" + ACCNO + "' AND STAGE<>'1004'   ";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;

    }//Dhanya shetty//21-03-2017
    public int jointupdate2(string BRCD, string GLCODE, string MID, string EDate, string Stage, string Srno, string Jname2, string Relation2, string DOB2, string SUBGLCODE, string CUSTNO, string ACCNO)
    {
        try
        {
            sql = "update JOINT set stage=1002, JOINTNAME='" + Jname2 + "',JOINTRELATION=  '" + Relation2 + "',JOINTDOB=  '" + conn.ConvertDate(DOB2).ToString() + "' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and JOINTSRNO='" + Srno + "' and  ACCNO='" + ACCNO + "' AND STAGE<>'1004'   ";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;

    }
    public int jointdelete1(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "delete from JOINT  WHERE BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SUBGLCODE + "' AND CUSTNO = '" + CUSTNO + "' and JOINTSRNO='" + Srno + "'  AND ACCNO='" + ACCNO + "' AND stage not in(1003,1004)";
            // sql = "Update JOINT Set STAGE = '1004' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and JOINTSRNO='" + Srno + "' and  ACCNO='" + ACCNO + "' AND STAGE not in(1003,1004)  ";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;
    }
    public int jointdelete2(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "delete from JOINT  WHERE BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SUBGLCODE + "' AND CUSTNO = '" + CUSTNO + "' and JOINTSRNO='" + Srno + "'  AND ACCNO='" + ACCNO + "' AND stage not in(1003,1004)";
            //sql = "Update JOINT Set STAGE = '1004' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and JOINTSRNO='" + Srno + "' and  ACCNO='" + ACCNO + "' AND STAGE not in(1003,1004)  ";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;
    }
    public int jointautho1(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO, string mid)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "Update JOINT Set STAGE = '1003' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and JOINTSRNO='" + Srno + "' and  ACCNO='" + ACCNO + "' AND MID <> '" + mid + "' AND STAGE<>'1004'   ";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;
    }
    public int jointautho2(string BRCD, string SUBGLCODE, string CUSTNO, string Srno, string ACCNO, string mid)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "Update JOINT Set STAGE = '1003' where  BRCD='" + BRCD + "' and SUBGLCODE= '" + SUBGLCODE + "'  and CUSTNO='" + CUSTNO + "'  and JOINTSRNO='" + Srno + "' and  ACCNO='" + ACCNO + "'  AND MID <> '" + mid + "' AND STAGE<>'1004'   ";
            joint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return joint;
    }
    public DataTable GetNominiInfo1(string BRCD, string custno, string AT, string srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select * from NOMINEEDETAILS where BRCD='" + BRCD + "'  and GLCODE='" + custno + "'  and SUBGLCODE='" + AT + "' and SRNO='" + srno + "' AND ACCNO='" + ACCNO + "' and  stage In ('1001','1002','1003')";

            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetNominiInfo2(string BRCD, string custno, string AT, string srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select * from NOMINEEDETAILS where BRCD='" + BRCD + "'  and GLCODE='" + custno + "'  and SUBGLCODE='" + AT + "' and SRNO='" + srno + "' AND ACCNO='" + ACCNO + "'  and  stage In ('1001','1002','1003')";

            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetJointinfo1(string BRCD, string custno, string AT, string srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select * from JOINT where BRCD='" + BRCD + "'  and GLCODE='" + custno + "'  and SUBGLCODE='" + AT + "' and JOINTSRNO='" + srno + "' AND ACCNO='" + ACCNO + "'  and  stage In ('1001','1002','1003')";

            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetJointinfo2(string BRCD, string custno, string AT, string srno, string ACCNO)//Dhanya shetty//21-03-2017
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "select * from JOINT where BRCD='" + BRCD + "'  and GLCODE='" + custno + "'  and SUBGLCODE='" + AT + "' and JOINTSRNO='" + srno + "'AND ACCNO='" + ACCNO + "'  and  stage In ('1001','1002','1003')";

            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetAgentName(string agentno, string BRCD)//Dhanya shetty//21-03-2017
    {
        try
        {
            sql = "select (AGENTNAME+'_'+Convert(VARCHAR(10),AGENTCODE)) AGENTNAME from AGENTMAST where brcd='" + BRCD + "' and AGENTCODE='" + agentno + "' And Stage In ('1001', '1002', '1003')";
            agentno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return agentno;
    }
    public int CheckCustno(string BRCD, string AT, string CustNo, string accno)//Dhanya shetty//23-03-2017
    {
        try
        {
            sql = "select isnull(count(*),0) from avs_acc where brcd='" + BRCD + "' and subglcode='" + AT + "' and custno='" + CustNo + "' and ACCNO='" + accno + "' and ACC_STATUS<>3";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex); ;
        }
        return Result;
    }

    public string GetCENTCUST()//// added by ankita on 03/07/2017
    {
        string custcnt = "";
        try
        {
            sql = "select listvalue from PARAMETER where LISTFIELD='CENTCUST'";
            custcnt = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custcnt;
    }
    public string GetcustnameYN(string listfld, string custno, string BRCD)//// added by ankita on 03/07/2017
    {
        try
        {
            if (listfld == "Y")
                sql = "SELECT (CUSTNAME+'_'+convert(varchar(10),Convert(int,CUSTNO))) CUSTNAME FROM MASTER WHERE CustNo = '" + custno + "' And Stage In ('1001', '1002', '1003')";
            else
                sql = "SELECT (CUSTNAME+'_'+convert(varchar(10),Convert(int,CUSTNO))) CUSTNAME FROM MASTER WHERE CustNo = '" + custno + "' And Stage In ('1001', '1002', '1003')";

            custno = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }


    public string GetcustnameYN(string listfld, string custno, string BRCD, string fontType)//// added by ankita on 03/07/2017
    {
        try
        {
            if (listfld == "Y")
                sql = "SELECT (CUSTNAME+'_'+convert(varchar(10),Convert(int,CUSTNO))) CUSTNAME FROM MASTER WHERE CustNo = '" + custno + "' And Stage In ('1001', '1002', '1003')";
            else
                sql = "SELECT (CUSTNAME+'_'+convert(varchar(10),Convert(int,CUSTNO))) CUSTNAME FROM MASTER WHERE CustNo = '" + custno + "' And Stage In ('1001', '1002', '1003')";

            custno = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }
    public int ModifyDDSPigmy(string brcd, string subgl, string custno, string accno, string openingdate, string duedate, string mid, string opr_type, string D_PERIOD, string D_AMOUNT, string RATEOFINT, string MATURITYAMT)//sp added by ankita 26/04/2017
    {
        try
        {
            sql = "EXEC SP_DDSPIGMY @FLAG='MD',@BRCD='" + brcd + "',@GLCODE='2',@SUBGLCODE='" + subgl + "',@CUSTNO='" + custno + "',@ACCNO='" + accno + "',@OPENINGDATE='" + openingdate + "',@DUEDATE='" + duedate + "',@MID='" + mid + "',@OPR_TYPE='" + opr_type + "',@D_PERIOD='" + D_PERIOD + "',@D_AMOUNT='" + D_AMOUNT + "',@RATEOFINT='" + RATEOFINT + "',@MATURITYAMT='" + MATURITYAMT + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int DeleteDDSPigmy(string brcd, string subgl, string accno, string custno)//sp added by ankita 26/04/2017
    {
        try
        {
            sql = "EXEC SP_DDSPIGMY @FLAG='DL',@BRCD='" + brcd + "',@GLCODE='2',@SUBGLCODE='" + subgl + "',@ACCNO='" + accno + "',@CUSTNO='" + custno + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int AuthoriseDDSPigmy(string brcd, string subgl, string accno, string mid, string custno)//sp added by ankita 26/04/2017
    {
        try
        {
            sql = "EXEC SP_DDSPIGMY @FLAG='AT',@BRCD='" + brcd + "',@GLCODE='2',@SUBGLCODE='" + subgl + "',@ACCNO='" + accno + "',@MID='" + mid + "',@CUSTNO='" + custno + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int UpdateLastno(string brcd, string subgl, string ACCNO)//added by ankita to update lastno 15/07/2017
    {
        try
        {
            sql = "select LASTNO from glmast where brcd='" + brcd + "' and SUBGLCODE='" + subgl + "'";
            sResult = conn.sExecuteScalar(sql);
            if (sResult != ACCNO)
            {
                sql1 = "update Glmast set LASTNO=(select (max(accno)) from Avs_Acc Where Brcd='" + brcd + "'  And Subglcode='" + subgl + "') Where Brcd='" + brcd + "' And Subglcode='" + subgl + "'";
                Res = conn.sExecuteQuery(sql1);
            }
            else
            {
                sql1 = "update Glmast set LASTNO=LASTNO-1 Where Brcd='" + brcd + "' And Subglcode='" + subgl + "'";
                Res = conn.sExecuteQuery(sql1);
            }


        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Res;
    }
    public int BindGrid(GridView Gview, string brcd, string EDate, string PT, string AC)
    {
        try
        {
            if (PT == "" && AC == "")
            {
                sql = "Select A.SUBGLCODE,A.ACCNO,B.CUSTNAME AS CUSTNAME,(case when A.Mid='999' then 'upload' else UU.USERNAME end) as MakerName from avs_acc A " +
                        " INNER JOIN MASTER B ON A.CUSTNO=B.CUSTNO Left JOIN USERMASTER UU ON  A.MID=Convert(varchar(10),UU.PERMISSIONNO) where A.stage not in (1004) and A.Brcd='" + brcd + "' " +
                        " And ACC_STATUS<>3 and A.OPENINGDATE='" + conn.ConvertDate(EDate) + "' order by accno,SUBGLCODE desc ";
            }
            else
            {
                sql = "Select A.SUBGLCODE,A.ACCNO,B.CUSTNAME AS CUSTNAME,(case when A.Mid='999' then 'upload' else UU.USERNAME end) as MakerName from avs_acc A " +
                     " INNER JOIN MASTER B ON A.CUSTNO=B.CUSTNO Left JOIN USERMASTER UU ON  A.MID=Convert(varchar(10),UU.PERMISSIONNO) where A.stage not in (1004) and A.Brcd='" + brcd + "' " +
                     " And ACC_STATUS<>3 and A.SUBGLCODE='" + PT + "' and A.ACCNO='" + AC + "' order by  accno,SUBGLCODE desc ";
            }
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int INSERTDATA(string accno, string brcd, string subgl, string glcode)
    {
        try
        {

            sql = "INSERT INTO depositinfo_history(CUSTNO,CUSTACCNO,DEPOSITGLCODE,PRNAMT,RATEOFINT,OPENINGDATE,DUEDATE,PERIOD,INTAMT,MATURITYAMT,BRCD,MID,PCMAC,LMSTATUS,STAGE) " +
                "select CUSTNO,ACCNO,SUBGLCODE,D_AMOUNT,'',OPENINGDATE,CLOSINGDATE,D_PERIOD,'','',BRCD,MID,PCMAC,'1','1001' from avs_acc where accno='" + accno + "' and subglcode='" + subgl + "' and glcode='" + glcode + "' and brcd='" + brcd + "' ";

            Res = conn.sExecuteQuery(sql);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
        return Res;
    }
    public string GETUSERGRP(string MID)
    {
        string usrgrp = "";
        try
        {
            sql = "select usergroup from usermaster where permissionno='" + MID + "'";
            usrgrp = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return usrgrp;
    }
    public string CheckDDSPara(string Brcd)//Dhanya Shetty//28/12/2017
    {
        try
        {
            sql = "select LISTVALUE from PARAMETER where LISTFIELD like '%DDSACCYN%' and BRCD='" + Brcd + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public bool CheckDDLPara(string Brcd)//Dhanya Shetty//28/12/2017
    {
        bool ecsParaCheck = false;
        try
        {
            sql = "select LISTVALUE from PARAMETER where LISTFIELD like '%ECS%' and BRCD='" + Brcd + "'";
            sResult = conn.sExecuteScalar(sql);
            if (sResult.ToUpper() == "Y")
            {
                ecsParaCheck = true;
            }
            else
            {
                ecsParaCheck = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ecsParaCheck;
    }

    public int ChkAccNoD(string brcd, string Subgl, String Acc)//Dhanya Shetty//29/12/2017
    {
        try
        {
            sql = "select  isnull(count(*),0) from AVS_ACC where brcd= '" + brcd + "' and subglcode='" + Subgl + "'   and accno='" + Acc + "' and acc_status<>3";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }
    public string OpenAccDDSN(string BRCD, string GLCODE)//Dhanya Shetty//09/01/2018
    {
        try
        {
            if (GLCODE == "2")
            {
                sql = "select (CASE WHEN LASTNO IS NULL THEN 1 ELSE (LASTNO+1) END) LASTNO from avs1000 WHERE type='DDSACCYN' and  BRCD='" + BRCD + "'";
            }

            AccNo = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AccNo;
    }
    public DataTable MasterNamejoint(string CUSTNO)//Dhanya Shetty//09/01/2018
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "SELECT CUSTNAME,CONVERT(VARCHAR(10),DOB,103)AS DOB FROM MASTER WHERE CUSTNO='" + CUSTNO + "'";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return dt;
    }
    public string StageJointCheck(string CUSTNO)//Dhanya Shetty//09/01/2018
    {
        string Result = "";
        try
        {
            sql = "SELECT STAGE FROM MASTER WHERE CUSTNO='" + CUSTNO + "'";
            Result = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return Result;
    }

    public string Getcustname_SHR(string custno)//ankita 22/11/2017 brcd removed
    {
        try
        {
            sql = "SELECT (M.CUSTNAME+'_'+(Convert(varchar(10),Convert(bigint,A.CUSTNO)))) From Avs_Acc A Inner join MASTER M On M.Custno = A.CustNo WHERE A.Subglcode = 4 And A.Brcd = 1 And A.Accno = '" + custno + "' ";
            custno = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }
    public string getcustdata(string CUSTNO)//ankita 28/03/2018
    {
        string Result = "";
        try
        {
            sql = "SELECT isnull(convert(varchar(10),CUSTTYPE),'0')+'_'+isnull(convert(varchar(10),DATEDIFF(YYYY,CONVERT(datetime,DOB),GETDATE())),'0') FROM MASTER WHERE CUSTNO='" + CUSTNO + "'";
            Result = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return Result;
    }
}