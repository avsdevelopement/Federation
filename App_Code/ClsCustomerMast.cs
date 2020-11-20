using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsCustomerMast
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";
    int Result = 0;
    public ClsCustomerMast()
    {

    }
    public int InsertMaster(string FL, string P_CUSTNO, string P_BRCD, string P_FIRSTNM, string P_LASTNM, string P_SURNM, string P_CUSTNM, string P_CUSTSEX, string P_DOB, string P_OCC, string P_OPENING, string P_MARRITALSTS, string P_NATIONALITY, string P_RESDENTAL, string P_FFNM, string P_FMINM, string P_FLSTNM, string P_MFNM, string P_MMNM, string P_MOLSTNM, string P_PREFIX, string P_MCID, string PropName, string RELIGIONCODE, string MARRIEGEDATE, string BLOODGROUP, string NO_OF_CHILD, string MEM_TYPE, string SPL_INSTRUCTION, string MEMBERNO, string CUSTCAST, string CUSTTYPE)
    {
        try
        {
            if (FL == "INSERT")
            {
                sql = "INSERT INTO MASTER (CUSTNO,BRCD,FIRSTNAME,LASTNAME,SURNAME,CUSTNAME,CUSTSEX,DOB,OCCUPATION,OPENINGDATE,MARRITALSTATUS,NATIONALITY,RESIDENTIAL,CFS_FIRSTNAME,CFS_MIDDLENAME,CFS_LASTNAME,CMO_FIRSTNAME,CMO_MIDDLENAME,CMO_LASTNAME,CPREFIX,STAGE,MID,APPLICANT1,RELIGIONCODE,MARRIEGEDATE,BLOODGROUP,NO_OF_CHILD,MEM_TYPE,SPL_INSTRUCTION,MEMBERNO,CUSTCAST,CUSTTYPE)  VALUES " +
                     "('" + P_CUSTNO + "','" + P_BRCD + "','" + P_FIRSTNM + "','" + P_LASTNM + "','" + P_SURNM + "','" + P_CUSTNM + "','" + P_CUSTSEX + "','" + conn.ConvertDate(P_DOB) + "','" + P_OCC + "','" + conn.ConvertDate(P_OPENING) + "','" + P_MARRITALSTS + "','" + P_NATIONALITY + "','" + P_RESDENTAL + "','" + P_FFNM + "','" + P_FMINM + "','" + P_FLSTNM + "','" + P_MFNM + "','" + P_MMNM + "','" + P_MOLSTNM + "','" + P_PREFIX + "','1001','" + P_MCID + "','" + PropName + "','" + RELIGIONCODE + "','" + MARRIEGEDATE + "','" + BLOODGROUP + "','" + NO_OF_CHILD + "','" + MEM_TYPE + "','" + SPL_INSTRUCTION + "','" + MEMBERNO + "','" + CUSTCAST + "','" + CUSTTYPE + "')";

                Result = conn.sExecuteQuery(sql);
                if (MEM_TYPE == "1" || MEM_TYPE == "0" || MEM_TYPE == "")
                {
                    if (Result > 0)
                    {
                        sql = "UPDATE AVS1000 SET LASTNO='" + P_CUSTNO + "' WHERE ACTIVITYNO='40' and Type='CUSTNO' AND BRCD='0'";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
                else if (MEM_TYPE == "2")
                {
                    if (Result > 0)
                    {
                        sql = "UPDATE AVS1000 SET LASTNO='" + P_CUSTNO + "' WHERE ACTIVITYNO='41' and Type='NOMCUSTNO'  AND BRCD='0'";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
                //if (Result > 0)
                //{
                //    sql = "UPDATE AVS1000 SET LASTNO='" + P_CUSTNO + "' WHERE ACTIVITYNO='40' AND BRCD='0'";
                //    Result = conn.sExecuteQuery(sql);
                //}
            }
            else if (FL == "UPDATE")
            {
                sql = "UPDATE MASTER SET FIRSTNAME='" + P_FIRSTNM + "',LASTNAME='" + P_LASTNM + "',SURNAME='" + P_SURNM + "',CUSTNAME='" + P_CUSTNM + "',CUSTSEX='" + P_CUSTSEX + "',DOB='" + conn.ConvertDate(P_DOB) + "',OCCUPATION='" + P_OCC + "',OPENINGDATE='" + conn.ConvertDate(P_OPENING) + "',MARRITALSTATUS='" + P_MARRITALSTS + "',NATIONALITY='" + P_NATIONALITY + "',RESIDENTIAL='" + P_RESDENTAL + "',CFS_FIRSTNAME='" + P_FFNM + "',CFS_MIDDLENAME='" + P_FMINM + "',CFS_LASTNAME='" + P_FLSTNM + "',CMO_FIRSTNAME='" + P_MFNM + "',CMO_MIDDLENAME='" + P_MMNM + "',CMO_LASTNAME='" + P_MOLSTNM + "',STAGE='1002',CID='" + P_MCID + "',APPLICANT1='" + PropName + "',RELIGIONCODE='" + RELIGIONCODE + "',MARRIEGEDATE='" + MARRIEGEDATE + "',BLOODGROUP='" + BLOODGROUP + "',NO_OF_CHILD='" + NO_OF_CHILD + "',MEM_TYPE='" + MEM_TYPE + "',SPL_INSTRUCTION='" + SPL_INSTRUCTION + "',MEMBERNO='" + MEMBERNO + "',CUSTCAST='" + CUSTCAST + "',CUSTTYPE='" + CUSTTYPE + "' WHERE CUSTNO='" + P_CUSTNO + "' ";

                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(P_CUSTNO);
    }
    public int DeleteMaster(string P_BRCD, string P_CUSTNO, string FL, string MID)
    {
        try
        {
            if (FL == "DL")
            {
                sql = "UPDATE MASTER SET STAGE='1004',VID='" + MID + "' WHERE CUSTNO='" + P_CUSTNO + "' ";
            }
            else
            {
                sql = "UPDATE MASTER SET STAGE='1003',VID='" + MID + "'  WHERE CUSTNO='" + P_CUSTNO + "'  AND MID <> '" + MID + "'";
            }
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return Result;
    }
    public string GetCustNoForNominal(string BRCD) //added By prasad for nominal members
    {
        try
        {
            sql = "SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE ACTIVITYNO=41 and Type='NOMCUSTNO' AND BRCD='" + BRCD + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public DataTable GetCustInfo(string BRCD, string custno,string FL)
    {
        DataTable DT = new DataTable();
        try 
        {
            if (FL == "VW")
            {
                sql = "SELECT CUSTNO,BRCD,APPLICANT1,FIRSTNAME,LASTNAME,SURNAME,CUSTNAME,CUSTSEX,DOB,OCCUPATION,OPENINGDATE,MARRITALSTATUS,NATIONALITY,RESIDENTIAL,CFS_FIRSTNAME,CFS_MIDDLENAME,CFS_LASTNAME,CMO_FIRSTNAME,CMO_MIDDLENAME,CMO_LASTNAME,Name,Name1,CPREFIX"+
               " RELIGIONCODE,MARRIEGEDATE,BLOODGROUP,NO_OF_CHILD,MEM_TYPE,SPL_INSTRUCTION,CUSTTYPE,CUSTCAST,MEMBERNO from MASTER M  " +
                 "  LEFT JOIN (SELECT DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  WHERE LNO='1023') LNO1 ON LNO1.ID=M.RESIDENTIAL " +
                  " LEFT JOIN ( SELECT DESCRIPTION Name1,SRNO id FROM LOOKUPFORM1  WHERE LNO='1003' ) LNO2 ON LNO2.ID=M.OCCUPATION " +
                   "WHERE  M.CUSTNO='" + custno + "' AND M.STAGE<>'1004' AND M.STAGE='1003'";
            }
            else
            {
                sql = "SELECT CUSTNO,BRCD,APPLICANT1,FIRSTNAME,LASTNAME,SURNAME,CUSTNAME,CUSTSEX,DOB,OCCUPATION,OPENINGDATE,MARRITALSTATUS,NATIONALITY,RESIDENTIAL,CFS_FIRSTNAME,CFS_MIDDLENAME,CFS_LASTNAME,CMO_FIRSTNAME,CMO_MIDDLENAME,CMO_LASTNAME,Name,Name1," +
               " RELIGIONCODE,MARRIEGEDATE,BLOODGROUP,NO_OF_CHILD,MEM_TYPE,SPL_INSTRUCTION,CUSTTYPE,CUSTCAST,MEMBERNO from MASTER M  " +
                 "  LEFT JOIN (SELECT DESCRIPTION Name,SRNO id FROM LOOKUPFORM1  WHERE LNO='1023') LNO1 ON LNO1.ID=M.RESIDENTIAL " +
                  " LEFT JOIN ( SELECT DESCRIPTION Name1,SRNO id FROM LOOKUPFORM1  WHERE LNO='1003' ) LNO2 ON LNO2.ID=M.OCCUPATION " +
                   "WHERE  M.CUSTNO='" + custno + "' AND M.STAGE<>'1004'";
            }
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return DT;
    }

    public string GetCustNo(string BRCD)
    {
        try
        {
            sql = "SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE ACTIVITYNO=40 and Type='CUSTNO' AND BRCD='" + BRCD + "'";
            //sql = "SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE ACTIVITYNO=40 AND BRCD='" + BRCD + "' and ";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return BRCD;
    }
    public string GetMemNo(string BRCD)
    {
        try
        {
            sql = "SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE ACTIVITYNO=41 and Type='MEMNO' AND BRCD='" + BRCD + "'";
            //sql = "SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE ACTIVITYNO=40 AND BRCD='" + BRCD + "' and ";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public string GetMemNo2(string BRCD)
    {
        try
        {
            sql = "SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE ACTIVITYNO=42 and Type='MEMNO' AND BRCD='" + BRCD + "'";
            //sql = "SELECT MAX(LASTNO)+1 FROM AVS1000 WHERE ACTIVITYNO=40 AND BRCD='" + BRCD + "' and ";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    public string CustNo(string BRCD)//Dhanya Shetty-For Log Detail

    {
        try
        {
            sql = "SELECT MAX(LASTNO) FROM AVS1000 WHERE ACTIVITYNO=40 AND BRCD='" + BRCD + "'";// for inserting records in avs500 
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }
    
    public string GetStage(string BRCD, string Custno)
    {
        try 
        {
        sql = "SELECT STAGE FROM MASTER WHERE  CUSTNO='" + Custno + "'";
        BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return BRCD;
    }

    public int InsertCustData(string P_CUSTNO, string P_BRCD, string P_FIRSTNM, string P_LASTNM, string P_SURNM, string P_CUSTNM, string P_CUSTSEX, string P_DOB, string P_OCC, string P_OPENING, string P_MARRITALSTS, string P_NATIONALITY, string P_RESDENTAL, string P_FFNM, string P_FMINM, string P_FLSTNM, string P_MFNM, string P_MMNM, string P_MOLSTNM, string P_PREFIX, string P_MCID, string P_PCNAME, string P_APPLNAME, string A_TxtLine1, string A_TxtLine2, string A_txttype, string A_txtrd, string A_txtopp, string A_State, string A_District, string A_Taluka, string A_City, string A_txtpin, string A_OPRStatus, string A_IDProof1, string A_TxtAno, string A_ADT, string C_Tel1, string C_Tel2, string C_Mob1, string C_Mob2, string C_WebSite, string C_EmailId, string I_IdProof, string I_DocNo, string I_DocDate, string add1, string add2, string add3, string add4, string parState, string parDist, string parTal, string parCity, string Flag, string RELIGIONCODE, string MARRIEGEDATE, string BLOODGROUP, string NO_OF_CHILD, string MEM_TYPE, string SPL_INSTRUCTION, string MEMBERNO, string CUSTCAST, string CUSTTYPE,string SAPNO,string BankAcc ,string Adharno)
    {
        try
        {
            if (Flag == "AD")
            {
                sql = "SP_CustAccountDetails @P_BRCD='" + P_BRCD + "',@P_CUSTNO='" + P_CUSTNO + "',@P_FIRSTNM='" + P_FIRSTNM + "',@P_LASTNM='" + P_LASTNM + "',@P_SURNM='" + P_SURNM + "',@P_CUSTNM='" + P_CUSTNM + "',@P_CUSTSEX='" + P_CUSTSEX + "',@P_DOB='" + conn.ConvertDate(P_DOB).ToString() + "',@P_OCC='" + P_OCC + "',@P_OPENING='" + conn.ConvertDate(P_OPENING).ToString() + "',@P_MARRITALSTS='" + P_MARRITALSTS + "',@P_NATIONALITY='" + P_NATIONALITY + "', " +
                      " @P_RESDENTAL='" + P_RESDENTAL + "',@P_FFNM='" + P_FFNM + "',@P_FMINM='" + P_FMINM + "',@P_FLSTNM='" + P_FLSTNM + "',@P_MFNM='" + P_MFNM + "',@P_MMNM='" + P_MMNM + "',@P_MOLSTNM='" + P_MOLSTNM + "',@P_PREFIX='" + P_PREFIX + "',@P_MID='" + P_MCID + "',@P_PCNAME='" + P_PCNAME + "',@P_APPLNAME='" + P_APPLNAME + "',@P_RELIGIONCODE='" + RELIGIONCODE + "',@P_MARRIEGEDATE='" + conn.ConvertDate(MARRIEGEDATE) + "',@P_BLOODGROUP='" + BLOODGROUP + "',"+
                      "@P_NO_OF_CHILD='" + NO_OF_CHILD + "',@P_MEM_TYPE='" + MEM_TYPE + "',@P_SPL_INSTRUCTION='" + SPL_INSTRUCTION + "',@P_MEMBERNO='" + MEMBERNO + "',@P_CUSTCAST='" + CUSTCAST + "',@P_CUSTTYPE='" + CUSTTYPE + "',@A_TxtLine1='" + A_TxtLine1 + "',@A_TxtLine2='" + A_TxtLine2 + "',@A_txttype='" + A_txttype + "', " +
                      " @A_txtrd='" + A_txtrd + "',@A_txtopp='" + A_txtopp + "',@A_State='" + A_State + "',@A_District='" + A_District + "',@A_Taluka='" + A_Taluka + "',@A_City='" + A_City + "',@A_PinCode='" + A_txtpin + "',@A_OPRStatus='" + A_OPRStatus + "',@A_IDProof1='" + A_IDProof1 + "',@A_DocNo1='" + A_TxtAno + "',@A_DocDate1='" + conn.ConvertDate(A_ADT).ToString() + "',@SAPNO='" + SAPNO + "',@BankAccNo='" + BankAcc + "',@AADHARCARDNO='" + Adharno + "', " +
                      " @C_Tel1='" + C_Tel1 + "',@C_Tel2='" + C_Tel2 + "',@C_Mob1='" + C_Mob1 + "',@C_Mob2='" + C_Mob2 + "',@C_WebSite='" + C_WebSite + "',@C_EmailId='" + C_EmailId + "',@I_IdProof2='" + I_IdProof + "',@I_DocNo2='" + I_DocNo + "',@I_DocDate2='" + conn.ConvertDate(I_DocDate).ToString() + "', " +
                      " @A_AddLine1='',@A_AddLine2='',@A_AddLine3='',@A_AddLine4='',@A_State1='',@A_District1='',@A_Taluka1='',@A_City1='',@Flag='AD'";
            }
            else if (Flag == "ADM")
            {
                sql = "SP_CustAccountDetails @P_BRCD='" + P_BRCD + "',@P_CUSTNO='" + P_CUSTNO + "',@P_FIRSTNM='" + P_FIRSTNM + "',@P_LASTNM='" + P_LASTNM + "',@P_SURNM='" + P_SURNM + "',@P_CUSTNM='" + P_CUSTNM + "',@P_CUSTSEX='" + P_CUSTSEX + "',@P_DOB='" + conn.ConvertDate(P_DOB).ToString() + "',@P_OCC='" + P_OCC + "',@P_OPENING='" + conn.ConvertDate(P_OPENING).ToString() + "',@P_MARRITALSTS='" + P_MARRITALSTS + "',@P_NATIONALITY='" + P_NATIONALITY + "', " +
                    " @P_RESDENTAL='" + P_RESDENTAL + "',@P_FFNM='" + P_FFNM + "',@P_FMINM='" + P_FMINM + "',@P_FLSTNM='" + P_FLSTNM + "',@P_MFNM='" + P_MFNM + "',@P_MMNM='" + P_MMNM + "',@P_MOLSTNM='" + P_MOLSTNM + "',@P_PREFIX='" + P_PREFIX + "',@P_MID='" + P_MCID + "',@P_PCNAME='" + P_PCNAME + "',@P_APPLNAME='" + P_APPLNAME + "',@P_RELIGIONCODE='" + RELIGIONCODE + "',@P_MARRIEGEDATE='" + conn.ConvertDate(MARRIEGEDATE) + "',@P_BLOODGROUP='" + BLOODGROUP + "'," +
                    "@P_NO_OF_CHILD='" + NO_OF_CHILD + "',@P_MEM_TYPE='" + MEM_TYPE + "',@P_SPL_INSTRUCTION='" + SPL_INSTRUCTION + "',@P_MEMBERNO='" + MEMBERNO + "',@P_CUSTCAST='" + CUSTCAST + "',@P_CUSTTYPE='" + CUSTTYPE + "',@A_TxtLine1='" + A_TxtLine1 + "',@A_TxtLine2='" + A_TxtLine2 + "',@A_txttype='" + A_txttype + "', " +
                    " @A_txtrd='" + A_txtrd + "',@A_txtopp='" + A_txtopp + "',@A_State='" + A_State + "',@A_District='" + A_District + "',@A_Taluka='" + A_Taluka + "',@A_City='" + A_City + "',@A_PinCode='" + A_txtpin + "',@A_OPRStatus='" + A_OPRStatus + "',@A_IDProof1='" + A_IDProof1 + "',@A_DocNo1='" + A_TxtAno + "',@A_DocDate1='" + conn.ConvertDate(A_ADT).ToString() + "',@SAPNO='" + SAPNO + "',@BankAccNo='" + BankAcc + "',@AADHARCARDNO='" + Adharno + "', " +
                    " @C_Tel1='" + C_Tel1 + "',@C_Tel2='" + C_Tel2 + "',@C_Mob1='" + C_Mob1 + "',@C_Mob2='" + C_Mob2 + "',@C_WebSite='" + C_WebSite + "',@C_EmailId='" + C_EmailId + "',@I_IdProof2='" + I_IdProof + "',@I_DocNo2='" + I_DocNo + "',@I_DocDate2='" + conn.ConvertDate(I_DocDate).ToString() + "', " +
                    " @A_AddLine1='',@A_AddLine2='',@A_AddLine3='',@A_AddLine4='',@A_State1='',@A_District1='',@A_Taluka1='',@A_City1='',@Flag='ADM'";
                //sql = "SP_CustAccountDetails  @P_BRCD='" + P_BRCD + "',@P_CUSTNO='" + P_CUSTNO + "',@P_FIRSTNM='" + P_FIRSTNM + "',@P_LASTNM='" + P_LASTNM + "',@P_SURNM='" + P_SURNM + "',@P_CUSTNM='" + P_CUSTNM + "',@P_CUSTSEX='" + P_CUSTSEX + "',@P_DOB='" + conn.ConvertDate(P_DOB).ToString() + "',@P_OCC='" + P_OCC + "',@P_OPENING='" + conn.ConvertDate(P_OPENING).ToString() + "',@P_MARRITALSTS='" + P_MARRITALSTS + "',@P_NATIONALITY='" + P_NATIONALITY + "', " +
                //       " @P_RESDENTAL='" + P_RESDENTAL + "',@P_FFNM='" + P_FFNM + "',@P_FMINM='" + P_FMINM + "',@P_FLSTNM='" + P_FLSTNM + "',@P_MFNM='" + P_MFNM + "',@P_MMNM='" + P_MMNM + "',@P_MOLSTNM='" + P_MOLSTNM + "',@P_PREFIX='" + P_PREFIX + "',@P_MID='" + P_MCID + "',@P_PCNAME='" + P_PCNAME + "',@P_APPLNAME='" + P_APPLNAME + "',@P_RELIGIONCODE='" + RELIGIONCODE + "',@P_MARRIEGEDATE='" + conn.ConvertDate(MARRIEGEDATE) + "',@P_BLOODGROUP='" + BLOODGROUP + "'," +
                //       "@P_NO_OF_CHILD='" + NO_OF_CHILD + "',@P_MEM_TYPE='" + MEM_TYPE + "',@P_SPL_INSTRUCTION='" + SPL_INSTRUCTION + "',@P_MEMBERNO='" + MEMBERNO + "',@P_CUSTCAST='" + CUSTCAST + "',@P_CUSTTYPE='" + CUSTTYPE + "',@A_TxtLine1='" + A_TxtLine1 + "',@A_TxtLine2='" + A_TxtLine2 + "',@A_txttype='" + A_txttype + "', " +
                //        " @A_txtrd='" + A_txtrd + "',@A_txtopp='" + A_txtopp + "',@A_State='" + A_State + "',@A_District='" + A_District + "',@A_Taluka='" + A_Taluka + "',@A_City='" + A_City + "',@A_PinCode='" + A_txtpin + "',@A_OPRStatus='" + A_OPRStatus + "',@A_IDProof1='" + A_IDProof1 + "',@A_DocNo1='" + A_TxtAno + "',@A_DocDate1='" + conn.ConvertDate(A_ADT).ToString() + "',@SAPNO='" + SAPNO + "',@BankAccNo='" + BankAcc + "',@AADHARCARDNO='" + Adharno + "', " +
                //      " '" + C_Tel1 + "','" + C_Tel2 + "','" + C_Mob1 + "','" + C_Mob2 + "','" + C_WebSite + "','" + C_EmailId + "','" + I_IdProof + "','" + I_DocNo + "','" + conn.ConvertDate(I_DocDate).ToString() + "'," +
                //      "  @A_AddLine1='" + add1 + "',@A_AddLine2='" + add2 + "',@A_AddLine3='" + add3 + "',@A_AddLine4='" + add4 + "',@A_State1'" + parState + "',@A_District1='" + parDist + "',@A_Taluka1='" + parTal + "',@A_City1='" + parCity + "',@Flag='ADM'";
            }
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        
        //return Convert.ToInt32(P_CUSTNO);
        //If Query gets error then also it showing Customer created -- Abhishek 29/05/2017
        
        if (Result > 0)
            return Convert.ToInt32(P_CUSTNO);
        else
            return -1;
    }

    public DataTable GetCustAccInfo(string BRCD, string custno, string Flag)
    {
        DataTable DT = new DataTable();
        try
        {
            if (Flag == "AT")
            {
                sql = "Exec SP_CustAccountDetails @P_BRCD = '" + BRCD + "', @P_CUSTNO = '" + custno + "', @Flag = 'GD'";
            }
            else
            {
                sql = "Exec SP_CustAccountDetails @P_BRCD = '" + BRCD + "', @P_CUSTNO = '" + custno + "', @Flag = '" + Flag + "'";
            }
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int AthorizeCustData(string P_CUSTNO, string P_BRCD, string P_MID)
    {
        try
        {
            sql = "SP_CustAccountDetails @P_BRCD = '" + P_BRCD + "', @P_CUSTNO = '" + P_CUSTNO + "', @P_MID = '" + P_MID + "', @Flag = 'AT'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(Result);
    }

    public int InsertCustMobile(string BrCode, string CustNo, string Mid, string EDate, string MobNum)
    {
        try
        {
            sql = "Insert Into AVS1091(BRCD, CUSTNO, MOBILE, DATE_OF_REG, STAGE, MID, VID, PCMAC, SYSTEMDATE)" +
                "Values('" + BrCode + "','" + CustNo + "','" + MobNum + "','" + conn.ConvertDate(EDate).ToString() + "','1003','" + Mid + "','" + Mid + "','" + conn.PCNAME() + "', GetDate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AddSMS_Desc(string BRCD, string CUSTNO, string MID, string EDT, string M_NUMBER, string FL)
    {
        try
        {
            if (FL == "ADD")
            {
                sql = "EXEC SP_SMS_INSERT @FLAG='" + FL + "',@CUSTNO='" + CUSTNO + "',@MOB_NUM='" + M_NUMBER + "',@SMS_DATE='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            }
            else if (FL == "MOD")
            {
                sql = "EXEC SP_SMS_INSERT @FLAG='" + FL + "',@CUSTNO='" + CUSTNO + "',@MOB_NUM='" + M_NUMBER + "',@SMS_DATE='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            }
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GETCUSTNAME(string fname,string mname,string lname)
    {
        DataTable dt= new DataTable();
        try
        {
            sql = "select FIRSTNAME,LASTNAME,SURNAME,CUSTNO from master where FIRSTNAME='"+fname+"' and SURNAME='"+lname+"' and LASTNAME='"+mname+"' and stage<>1004";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetMDetails(string BRCD, string FDate, string TDate)//Dhanya shetty //21/08/2017
    {
        sql = "EXEC Isp_AVS0048 '" + BRCD + "','" + conn.ConvertDate(FDate) + "','" + conn.ConvertDate(TDate) + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
}