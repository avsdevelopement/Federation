using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public class ClsAVS5029
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string divname = "", sql = "", sql1 = "", sql2 = "", sql3="",value = "";
    int result = 0, result1 = 0, result2 = 0, result3=0,result4=0;
    string sResult = "";

	public ClsAVS5029()
	{
		
	}

    public string GetDivName(string recdiv)
    {
        try
        {
            sql = "select DESCR from paymast where RECDIV='" + recdiv + "' and RECCODE='0'";
            divname = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return divname;
    }

    public string GetOffcName(string recdiv,string reccode)
    {
        try
        {
            sql = "select DESCR from paymast where RECDIV='" + recdiv + "' and RECCODE='"+reccode+"'";
            divname = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return divname;
    }

    public string GetDivNo(string recdiv)
    {
        try
        {
            sql = "select RECDIV from paymast where DESCR='" + recdiv + "' and RECCODE='0'";
            divname = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return divname;
    }

    public string GetOffcNo(string recdiv, string reccode)
    {
        try
        {
            sql = "select RECCODE from paymast where RECDIV='" + recdiv + "' and DESCR='" + reccode + "'";
            divname = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return divname;
    }

    public int Insert(string CUSTNO, string EMPNO,string Mob, string NAMEOFDIV, string NAMEOFOFFC, string DOJ, string DOR, string RTGAGE, string BRCD, string MID, string DESIGNATION, string DOC,string DIVNO,string OFFNO,string SAPNO,string adhar,string pan)
    {
        try
        {
            sql = "insert into EMPDETAIL(CUSTNO,  EMPNO, DIVNO, NAMEOFDIV,OFFNO,  NAMEOFOFFC,  DOJ,  DOR,  RTGAGE, STAGE, BRCD,  MID,  DESIGNATION,  DOC,SAPNO,ADHARCARD,PANCARD) values('" + CUSTNO + "','" + EMPNO + "','" + DIVNO + "','" + NAMEOFDIV + "','" + OFFNO + "','" + NAMEOFOFFC + "','" + conn.ConvertDate(DOJ) + "','" + conn.ConvertDate(DOR) + "','" + RTGAGE + "','1001','" + BRCD + "','" + MID + "','" + DESIGNATION + "','" + conn.ConvertDate(DOC) + "','" + SAPNO + "','"+adhar+"','"+pan+"')";
            result = conn.sExecuteQuery(sql);
            sql1 = "update MASTER set RECDEPT='" + OFFNO + "',BRANCHNAME='" + DIVNO + "',EMPNO='" + EMPNO + "',SAPNO='" + SAPNO + "',Mobile='" + Mob + "' where custno='" + CUSTNO + "'";
            result1 = conn.sExecuteQuery(sql1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int Modify(string id, string CUSTNO, string EMPNO, string NAMEOFDIV, string NAMEOFOFFC, string DOJ, string DOR, string RTGAGE, string BRCD, string MID, string DESIGNATION, string DOC, string DIVNO, string OFFNO, string SAPNO, string AGE, string EMAILID, string BLOODGRP, string ADHAR,  string CUSTTYPE, string MobNo,string Pancard,string MemMobNo)
    {
        try
        {
            sql = "Insert into EMPDETAIL_H(CUSTNO,  EMPNO, DIVNO, NAMEOFDIV,OFFNO,  NAMEOFOFFC,  DOJ,  DOR,  RTGAGE, STAGE, BRCD ,   MID,cid, VID,PCMAC, DESIGNATION,  DOC,SAPNO) select CUSTNO,  EMPNO, DIVNO, NAMEOFDIV,OFFNO,  NAMEOFOFFC,  DOJ,  DOR,  RTGAGE, '1002', BRCD ,   MID,cid, VID,PCMAC,  DESIGNATION,  DOC, SAPNO from EMPDETAIL where custno='" + CUSTNO + "' and brcd='" + BRCD + "' AND STAGE<>'1004' and ID='" + id + "'";
            result2 = conn.sExecuteQuery(sql);
            sql = "update EMPDETAIL set EMPNO='" + EMPNO + "',DIVNO='" + DIVNO + "',  NAMEOFDIV='" + NAMEOFDIV + "', OFFNO='" + OFFNO + "', NAMEOFOFFC='" + NAMEOFOFFC + "',  DOJ='" + conn.ConvertDate(DOJ) + "',  DOR='" + conn.ConvertDate(DOR) + "',  RTGAGE='" + RTGAGE + "',  STAGE='1002',  BRCD='" + BRCD + "',  VID='" + MID + "',  DESIGNATION='" + DESIGNATION + "',  DOC='" + conn.ConvertDate(DOC) + "',SAPNO='" + SAPNO + "'  where custno='" + CUSTNO + "' AND STAGE<>'1004' and ID='" + id + "'";
            result = conn.sExecuteQuery(sql);
            sql1 = "update MASTER set RECDEPT='" + OFFNO + "',BRANCHNAME='" + DIVNO + "',EMPNO='" + EMPNO + "',SAPNO='" + SAPNO + "',EMAILID='" + EMAILID + "',BLOODGROUP='" + BLOODGRP + "',CUSTTYPE='" + CUSTTYPE + "',CUSTAGE='" + AGE + "',MOBILE='" + MemMobNo + "' where custno='" + CUSTNO + "'";

           // sql1 = "update MASTER set RECDEPT='" + OFFNO + "',BRANCHNAME='" + DIVNO + "',EMPNO='" + EMPNO + "' where custno='" + CUSTNO + "'";

            result1 = conn.sExecuteQuery(sql1);
            sql2 = "update NOMINEEDETAILS set MobNo='" + MobNo + "' where custno='" + CUSTNO + "' and glcode='4'";
            result3 = conn.sExecuteQuery(sql2);
           // sql3 = " update identity_proof set  ADHARCARD='" + ADHAR + "',PANCARD='" + Pancard + "' where custno='" + CUSTNO + "'";
            sql3 = "update identity_proof set DOC_NO ='" + Pancard + "' where custno='" + CUSTNO + "' and DOC_TYPE='3'";
         string    sql4 = "update identity_proof set DOC_NO ='" + ADHAR + "' where custno='" + CUSTNO + "' and DOC_TYPE='3'";
         result4 = conn.sExecuteQuery(sql3 + sql4);
         string sql5 = "UPDATE AVS_DEDTRANX set recdiv='" + OFFNO + "' ,reccode='" + DIVNO + "'    WHERE Status =1 AND         CustNo='" + CUSTNO + "'    AND Stage<>1004";
         result4 = conn.sExecuteQuery(sql5);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int Delete(string id, string CUSTNO, string BRCD, string MID)
    {
        try
        {
            sql = "Insert into EMPDETAIL_H(CUSTNO,  EMPNO, DIVNO, NAMEOFDIV,OFFNO,  NAMEOFOFFC,  DOJ,  DOR,  RTGAGE, STAGE, BRCD,  MID,cid, VID,PCMAC, DESIGNATION,  DOC,SAPNO) select CUSTNO,  EMPNO, DIVNO, NAMEOFDIV,OFFNO,  NAMEOFOFFC,  DOJ,  DOR,  RTGAGE, '1004', BRCD,  MID,cid, VID,PCMAC,  DESIGNATION,  DOC,SAPNO from EMPDETAIL where custno='" + CUSTNO + "' and brcd='" + BRCD + "' AND STAGE<>'1004' and ID='" + id + "'";
            result2 = conn.sExecuteQuery(sql);
            sql = "update EMPDETAIL set STAGE='1004', VID='" + MID + "' where custno='" + CUSTNO + "' AND STAGE<>'1004' and ID='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int Authorise(string id, string CUSTNO, string BRCD, string MID)
    {
        try
        {
            sql = "update EMPDETAIL set STAGE='1003', VID='" + MID + "' where custno='" + CUSTNO + "' AND STAGE<>'1004' and ID='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public string CheckSurety(string BrCd, string LoanGlCode, string LoanAccNo, string LoanCustNo, string SuretyCustNo, string RecType)
    {
        try
        {
            sql = "Select Count(1) From AVS6002 Where LoanBrCd = '" + BrCd + "' And LoanGlCode = '" + LoanGlCode + "' And " +
                  "LoanAccNo = '" + LoanAccNo + "' And LoanCustNo = '" + LoanCustNo + "' And SuretyCustNo = '" + SuretyCustNo + "' And RecType = '" + RecType + "' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }

    public DataTable GetSuretyDetails(string BrCd, string LoanGlCode, string LoanAccNo, string LoanCustNo, string SuretyCustNo)
    {
        try
        {
            sql = "Select * From AVS6002 Where LoanBrCd = '" + BrCd + "' And LoanGlCode = '" + LoanGlCode + "' And " +
                  "LoanAccNo = '" + LoanAccNo + "' And LoanCustNo = '" + LoanCustNo + "' And SuretyCustNo = '" + SuretyCustNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public int StartSurety(string BrCd, string LoanGlCode, string LoanAccNo, string LoanCustNo, string SuretyCustNo, string PrinAmt, string IntAmt, string Mid)
    {
        try
        {
            sql = "Select Count(1) From AVS6002 Where LoanBrCd = '" + BrCd + "' And LoanGlCode = '" + LoanGlCode + "' And " +
                  "LoanAccNo = '" + LoanAccNo + "' And LoanCustNo = '" + LoanCustNo + "' And SuretyCustNo = '" + SuretyCustNo + "' ";
            sResult = conn.sExecuteScalar(sql);

            if (sResult == "0")
            {
                sql = "Insert Into AVS6002(LoanBrCd, LoanGlCode, LoanAccNo, LoanCustNo, SuretyCustNo, Principle, Interest, Mid, Stage, RecType, SystemDate) " +
                      "Values('" + BrCd + "', '" + LoanGlCode + "', '" + LoanAccNo + "', '" + LoanCustNo + "', '" + SuretyCustNo + "', '" + PrinAmt + "', '" + IntAmt + "', '" + Mid + "', '1003', '1', GetDate())";
                result = conn.sExecuteQuery(sql);
            }
            else
            {
                sql = "Update AVS6002 Set Principle = '" + PrinAmt + "', Interest = '" + IntAmt + "', RecType = '1'" +
                      "Where LoanBrCd = '" + BrCd + "' And LoanGlCode = '" + LoanGlCode + "' And LoanAccNo = '" + LoanAccNo + "' " +
                      "And LoanCustNo = '" + LoanCustNo + "' And SuretyCustNo = '" + SuretyCustNo + "' ";
                result = conn.sExecuteQuery(sql);
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int StopSurety(string BrCd, string LoanGlCode, string LoanAccNo, string LoanCustNo, string SuretyCustNo, string Mid)
    {
        try
        {
            sql = "Update AVS6002 Set RecType = '2', Vid = '" + Mid + "' Where LoanBrCd = '" + BrCd + "' And LoanGlCode = '" + LoanGlCode + "' And " +
                  "LoanAccNo = '" + LoanAccNo + "' And LoanCustNo = '" + LoanCustNo + "' And SuretyCustNo = '" + SuretyCustNo + "' ";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public void Bind(GridView grd, string CUSTNO, string BRCD)
    {
        try
        {
            sql = "SELECT E.ID, E.BRCD, E.CUSTNO, ConVert(VarChar(10), M.OPENINGDATE, 103) As OPENINGDATE, ConVert(VarChar(10), M.DOB, 103) As DOB, ConVert(VarChar(10), E.DOJ, 103) As DOJ, " +
                 "ConVert(VarChar(10), E.DOR, 103) As DOR, ConVert(Varchar(10), E.DOC, 103) As DOC, E.DESIGNATION, E.EMPNO, E.DIVNO, E.NAMEOFDIV, E.OFFNO, E.NAMEOFOFFC, E.RTGAGE,  E.MID, " +
                 "(ConVert(VarChar(10), E.DIVNO) +'-'+ E.NAMEOFDIV) As DIV, (ConVert(VarChar(10), E.OFFNO) +'-'+ E.NAMEOFOFFC) As OFFC " +
                 "FROM EMPDETAIL E With(NoLock) " +
                 "Inner Join Master M With(NoLock) On E.CustNo = M.CustNo " +
                 "WHERE E.CustNo = '" + CUSTNO + "' and E.Stage <> '1004'";
            conn.sBindGrid(grd,sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public DataTable GetDetails(string ID)
    {
        DataTable dt = new DataTable();
        try
        {
          ////  sql = "SELECT E.BRCD, E.CUSTNO, Convert(Varchar(10), M.OPENINGDATE, 103) As OPENINGDATE, Convert(Varchar(10), M.DOB, 103) As DOB, Convert(Varchar(10), E.DOJ, 103) As DOJ, " +
          // //       "Convert(Varchar(10), E.DOR, 103) As DOR, Convert(Varchar(10), E.DOC, 103) As DOC, E.DESIGNATION, E.EMPNO, E.DIVNO, E.NAMEOFDIV, E.OFFNO, E.NAMEOFOFFC, E.RTGAGE,  E.MID, " +
          //        "E.BankName, E.BranchName, E.Accno, E.IFSCcd,E.SAPNO " +
          //        "FROM EMPDETAIL E With(NoLock) " +
          //        "Inner Join Master M With(NoLock) On E.CustNo = M.CustNo " +
              //    "WHERE E.ID = '" + ID + "' and E.Stage <> '1004'";

            //sql = "SELECT E.BRCD, E.CUSTNO, Convert(Varchar(10), M.OPENINGDATE, 103) As OPENINGDATE, Convert(Varchar(10), M.DOB, 103) As DOB, Convert(Varchar(10), E.DOJ, 103) As DOJ," +
            //  " Convert(Varchar(10), E.DOR, 103) As DOR, Convert(Varchar(10), E.DOC, 103) As DOC, E.DESIGNATION,M.EMAILID,M.BLOODGROUP,E.ADHARCARD,M.CUSTTYPE, M.CUSTAGE,  E.EMPNO, E.DIVNO, N.MobNo,E.PANCARD,M.Mobile " +
            //"  E.NAMEOFDIV, E.OFFNO, E.NAMEOFOFFC, E.RTGAGE,  E.MID, E.BankName, E.BranchName,E.Accno, E.IFSCcd,E.SAPNO FROM EMPDETAIL E With(NoLock) left Join NOMINEEDETAILS N With(NoLock) On  E.CustNo = N.CustNo Inner Join Master M With(NoLock) " +
            //   "On E.CustNo = M.CustNo WHERE E.ID =  '" + ID + "' and E.Stage <> '1004'";
            sql="		SELECT E.BRCD, E.CUSTNO, Convert(Varchar(10), M.OPENINGDATE, 103) As OPENINGDATE, Convert(Varchar(10), M.DOB, 103) As DOB, Convert(Varchar(10), E.DOJ, 103) As DOJ,"+
               " Convert(Varchar(10), E.DOR, 103) As DOR, Convert(Varchar(10), E.DOC, 103) As DOC, E.DESIGNATION,M.EMAILID,M.BLOODGROUP,M.CUSTTYPE, M.CUSTAGE,  E.EMPNO, E.DIVNO, N.MobNo,M.Mobile, "+
                "E.NAMEOFDIV, E.OFFNO, E.NAMEOFOFFC, E.RTGAGE,  E.MID, E.BankName, E.BranchName,E.Accno, E.IFSCcd,E.SAPNO,AD.DOC_NO Adharcard,PN.DOC_NO Pancard FROM EMPDETAIL E With(NoLock)"+
                 "left Join NOMINEEDETAILS N With(NoLock) On  E.CustNo = N.CustNo Inner Join Master M With(NoLock) On E.CustNo = M.CustNo  Inner Join AVS_Acc A On A.CustNo=M.CustNO And A.Glcode='4' Left Join  (select DOC_NO,CustNo from identity_proof where DOC_TYPE='2' ) AD" +
                " on AD.Custno=E.Custno Left Join  (select DOC_NO,CustNo from identity_proof where DOC_TYPE='3' )PN on PN.CustNo=E.CustNo WHERE E.ID = '" + ID + "'   and E.Stage <> '1004' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public string GetMid(string ID)
    {
        try
        {
            sql = "SELECT MID FROM EMPDETAIL WHERE ID='" + ID + "' and stage<>'1004'";
            divname = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return divname;
    }

    public string GetStage(string ID)
    {
        string STAGE = "";
        try
        {
            sql = "SELECT STAGE FROM EMPDETAIL WHERE ID='" + ID + "' and stage<>'1004'";
            STAGE = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return STAGE;
    }

    public int ChkExist(string custno)
    {
        try
        {
            sql = "select count(*) from EMPDETAIL where custno='" + custno + "' and stage<>'1004'";
            result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public string getDesigValue(string design)
    {
        try
        {
            sql = "select srno from lookupform1 where description='" + design + "' and lno='1101'";
            value = conn.sExecuteScalar(sql);          
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return value;
    }

    public int Insertbnk(string brcd,string custno,string srno,string bankname,string branchname,string Accno,string IFSCCode,string mid)
    {
        try
        {
            sql = "insert into BankDetails(brcd,custno,srno,bankname,branchname,Accno,IFSCCode,stage,mid) values('" + brcd + "','" + custno + "','" + srno + "','" + bankname + "','" + branchname + "','" + Accno + "','" + IFSCCode + "','1001','" + mid + "')";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int Modifybnk(string brcd, string custno, string srno, string bankname, string branchname, string Accno, string IFSCCode, string mid)
    {
        try
        {
            sql = "update BankDetails set custno='" + custno + "',  srno='" + srno + "', bankname='" + bankname + "', branchname='" + branchname + "',  Accno='" + Accno + "',  IFSCCode='" + IFSCCode + "',  vid='" + mid + "',  STAGE='1002' where custno='" + custno + "' AND STAGE<>'1004' and  srno='" + srno + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int Deletebnk(string CUSTNO, string BRCD, string MID)
    {
        try
        {
            sql = "update BankDetails set STAGE='1004', VID='" + MID + "' where custno='" + CUSTNO + "' AND STAGE<>'1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int Authorisebnk(string CUSTNO, string BRCD, string MID)
    {
        try
        {
            sql = "update BankDetails set STAGE='1003', VID='" + MID + "' where custno='" + CUSTNO + "' AND STAGE<>'1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public DataTable GetDetailsBnk(string custno, string BRCD)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "SELECT brcd,custno,srno,bankname,branchname,Accno,IFSCCode,stage,mid FROM BankDetails WHERE custno='" + custno + "' AND STAGE<>'1004'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public int getBnkCnt(string custno, string BRCD)
    {
        try
        {
            sql = "select count(*) from BankDetails where custno='" + custno + "' AND STAGE<>'1004'";
            result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public int deletebnk(string custno, string BRCD)
    {
        try
        {
            sql = "delete from BankDetails where custno='" + custno + "' AND STAGE<>'1004'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

}