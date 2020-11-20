using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public class ClsAVS5027
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "", sResult = "", App = "";
    int Result = 0, result;
    int RC;

    public ClsAVS5027()
    {

    }

    public string GetParameter(string BrCode, string ListField)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '" + BrCode + "' And ListField = '" + ListField + "' And Stage = '1003'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetDetails(string brcd, string prdno, string custno)
    {
        try
        {
            sql = "select Convert(varchar(10),DOJ,103) from EMPDETAIL where brcd='" + brcd + "'    and custno='" + custno + "' and stage<>1004";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetMember(string custno)
    {
        try
        {
            sql = "select accno from avs_acc where brcd=1 and glcode=4 and custno='" + custno + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetMembrship(string brcd, string prd)
    {
        try
        {
            sql = "select LOANGLBALANCE,ROI from loangl where brcd='" + brcd + "' and Loanglcode='" + prd + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string LoanDetails(string BrCode, string PrCode)
    {
        string Result = "";
        try
        {
            sql = "Select ConVert(VarChar(10), Period) + '_' + ConVert(VarChar(10), ROI) From LoanGl Where BrCd = '" + BrCode + "' And LoanGlCode = '" + PrCode + "' ";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Insertdata(string brcd, string Ltype, string Lname, string purpose, string Lapply, string Grosssal, string netsal, string twntyfive, string bymembership,
        string bysal, string sanctionamt, string repaycap, string installment, string interest, string totalinst, string stage, string mid, string appdate,
        string Cno, string Memno, string appstatus, string InstType, string IntRate, string Period, string Sanction, string ReCon, string value)
    {
        try
        {
            sql = "Insert Into AVS1004(BRCD, LOANPRODUCT, LOANTYPE, LOANPURPOSE, LOANAPPLY, GROSSSAL, NETSAL, TWENTYFIVE, LOANELGIBLITYMEMBER, LOANELGIBLITYSALARY, " +
                  "SANCITONAMOUNT, REPAYCAP, INSTMANUAL, INTEREST, TOTINT, STAGE, MID, APP_DATE, CUSTNO, MEMNO, APPSTATUS, InstType, IntRate, Period, Sanction, Recom, LoanDoc) " +
                  "Values ('" + brcd + "', '" + Ltype + "', '" + Lname + "','" + purpose + "', '" + Lapply + "', '" + Grosssal + "', '" + netsal + "', '" + twntyfive + "', " +
                  "'" + bymembership + "', '" + bysal + "', '" + sanctionamt + "', '" + repaycap + "', '" + installment + "', " + Period + ", " + totalinst + ", '" + stage + "', " +
                  "'" + mid + "', '" + conn.ConvertDate(appdate).ToString() + "', '" + Cno + "', '" + Memno + "', '" + appstatus + "', '" + InstType + "', '" + IntRate + "', '" + Period + "', " +
                  "'" + Sanction + "', '" + ReCon + "', '" + value + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void InsertLoan(string BRCD, string LType, string Lname, string custno, string Amount, string MID)
    {
        try
        {
            sql = "insert into avs1005 (BRCD,CUSTNO,DEDTYPE,SRNO,PRODUCT,CHQPRINTNAME,DEBIT,APPSTATUS,STAGE,MID,CID,VID,LoanType) values ('" + BRCD + "','" + custno + "',1,1,'" + LType + "','" + Lname + "','" + Amount + "',1,1001,'" + MID + "','" + MID + "','" + MID + "','" + Lname + "')";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public int GetCust(string brcd, string EDate, string srno, string custno, string Scust, string memno, string name, string memdate, string retiredate, string loanbal, string Remser, string stands, string stage, string mid, string appstatus, string prd)
    {
        try
        {
            sql = "Insert into AVS1003(BRCD,SURITYSRNO,CUSTNO,ACCNO,MEM_NO_SURITY,SURITYNAME,MEMBERDATE,DATEOFRET,BAOFSURITY,REMSERVICE,STANDS,STAGE,MID,APPSTATUS,PRODUCT) VALUES('" + brcd + "','" + srno + "'," +
                "'" + custno + "','" + Scust + "','" + memno + "','" + name + "','" + conn.ConvertDate(memdate).ToString() + "','" + conn.ConvertDate(retiredate).ToString() + "', '" + loanbal + "'," + Remser + "," + stands + ",'" + stage + "'," +
                "'" + mid + "','" + appstatus + "','" + prd + "')";
            Result = conn.sExecuteQuery(sql);

            //  Mobile number updation (if not exixts then create)
            UpdateMobile(brcd, Scust, stands, EDate, mid);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GetPrev(string brcd, string srno, string prd, string intrst, string name, string stage, string mid, string dedtype, string appstatus, string custno, string Type, string LoanType, string ACCNO, string Bal)
    {
        try
        {
            sql = "Insert into AVS1005(BRCD,SRNO,PRODUCT,CREDIT,CHQPRINTNAME,STAGE,MID,DEDTYPE,APPSTATUS,CUSTNO,TransType,LoanType,AccNo,Bal) VALUES('" + brcd + "','" + srno + "','" + prd + "','" + intrst + "','" + name + "'," +
                "'" + stage + "','" + mid + "','" + dedtype + "','" + appstatus + "','" + custno + "'," + Type + ",'" + LoanType + "','" + ACCNO + "','" + Bal + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GetPrevDed(string brcd, string srno, string prd, string Ded, string name, string stage, string mid, string dedtype, string appstatus, string custno, string Type, string LoanType, string ACCNO, string Bal)
    {
        try
        {
            sql = "Insert into AVS1005(BRCD,SRNO,PRODUCT,CREDIT,CHQPRINTNAME,STAGE,MID,DEDTYPE,APPSTATUS,CUSTNO,TransType,LoanType,AccNo,Bal) VALUES('" + brcd + "','" + srno + "','" + prd + "','" + Ded + "','" + name + "'," +
                "'" + stage + "','" + mid + "','" + dedtype + "','" + appstatus + "','" + custno + "'," + Type + ",'" + LoanType + "','" + ACCNO + "','" + Bal + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GetStandard(string brcd, string srno, string prd, string deductn, string name, string stage, string mid, string dedtype, string appstatus, string custno, string Type, string LoanType, string Per, string Accno)
    {
        try
        {
            sql = "Insert into AVS1005(BRCD,SRNO,PRODUCT,CREDIT,CHQPRINTNAME,STAGE,MID,DEDTYPE,APPSTATUS,CUSTNO,TransType,LoanType,percentage,accno) VALUES('" + brcd + "','" + srno + "','" + prd + "','" + deductn + "','" + name + "'," +
                "'" + stage + "','" + mid + "','" + dedtype + "','" + appstatus + "','" + custno + "'," + Type + ",'" + LoanType + "','" + Per + "','" + Accno + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GetOther(string brcd, string srno, string prd, string deductn, string name, string stage, string mid, string dedtype, string appstatus, string custno, string Type, string LoanType, string accno)
    {
        try
        {
            sql = "Insert into AVS1005(BRCD,SRNO,PRODUCT,CREDIT,CHQPRINTNAME,STAGE,MID,DEDTYPE,APPSTATUS,CUSTNO,TransType,LoanType,accno) VALUES('" + brcd + "','" + srno + "','" + prd + "','" + deductn + "','" + name + "'," +
                "'" + stage + "','" + mid + "','" + dedtype + "','" + appstatus + "','" + custno + "'," + Type + ",'" + LoanType + "','" + accno + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GetTotded(string brcd, string prd, string deductn, string name, string stage, string mid, string dedtype, string appstatus, string custno, string Type)
    {
        try
        {
            sql = "Insert into AVS1005(BRCD,PRODUCT,DEBIT,CHQPRINTNAME,STAGE,MID,DEDTYPE,APPSTATUS,CUSTNO,TransType) VALUES('" + brcd + "','" + prd + "','" + deductn + "','" + name + "'," +
                "'" + stage + "','" + mid + "','" + dedtype + "','" + appstatus + "','" + custno + "'," + Type + ")";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string Appno(string prd, string BRCD)
    {
        try
        {
            sql = "select ISNULL(max(lastno),0)+1 from avs1000 where ACTIVITYNO='" + prd + "' and type='APPNO' and BRCD='" + BRCD + "'";
            App = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return App;
    }
    public string BondNo(string prd, string BRCD)
    {
        try
        {
            sql = "select ISNULL(max(lastno),0)+1 from avs1000 where ACTIVITYNO='" + prd + "' and type='BONDNO' and BRCD='" + BRCD + "'";
            App = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return App;
    }
    public int updateapp(string appno, string prd, string BRCD)
    {
        try
        {
            sql = "update avs1000 set lastno='" + appno + "'  where ACTIVITYNO='" + prd + "' and type='APPNO' and brcd='" + BRCD + "'";
            RC = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RC;
    }
    public int updateBond(string Bnd, string prd, string BRCD)
    {
        try
        {
            sql = "update avs1000 set lastno='" + Bnd + "'  where ACTIVITYNO='" + prd + "' and type='BONDNO' and brcd='" + BRCD + "'";
            RC = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RC;
    }

    public string GetMobileNo(string CustNo)
    {
        try
        {
            sql = "Select IsNull(A.Mobile1, A.Mobile2) As Mobile From AVS_CONTACTD A " +
                  "Where A.CustNo = '" + CustNo + "' And A.Stage = '1003' " +
                  "And A.EffectDate = (Select Max(EffectDate) From AVS_CONTACTD Where CustNo = A.CustNo And Stage = A.Stage)";

            //sql = "select count(MemberNo) from AVSLnSurityTable where MemberNo='" + CustNo + "' and stage<>1004";
            sResult = conn.sExecuteScalar(sql);

            if (sResult == null)
                sResult = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int UpdateMobile(string BrCode, string CustNo, string MobileNo, string EDate, string Mid)
    {
        try
        {
            sql = "Select Count(1) As Count From AVS_CONTACTD Where CustNo = '" + CustNo.ToString() + "' And Stage != 1004 ";
            sResult = conn.sExecuteScalar(sql);

            if (Convert.ToDouble(sResult) == 0 && MobileNo.ToString() != "")
            {
                sql = "Insert Into AVS_CONTACTD(BrCd, CustNo, Mobile1, Stage, Mid, EffectDate, SystemDate) " +
                  "Values('" + BrCode + "', '" + CustNo + "', '" + MobileNo + "', '1003', '" + Mid + "', '" + conn.ConvertDate(EDate) + "', GetDate())";
                Result = conn.sExecuteQuery(sql);
            }
            else if (MobileNo.ToString() != "")
            {
                sql = "Update AVS_CONTACTD Set Mobile1 = '" + MobileNo + "', EffectDate = '" + conn.ConvertDate(EDate) + "', STAGE = '1003', MID = '" + Mid + "' " +
                      "Where CustNo = '" + CustNo + "' And Stage != 1004 ";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetAccNo(string brcd, string custno)
    {
        try
        {
            sql = "select accno from avs_acc where brcd='" + brcd + "' and  custno='" + custno + "' and acc_status<>3 and glcode=3 and stage<>1004";
            App = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return App;
    }

    public string GetIrNo(string brcd, string prd)
    {
        try
        {
            sql = "select IR from glmast where brcd='" + brcd + "' and  subglcode='" + prd + "'";
            App = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex); ;
        }
        return App;
    }

    public int BindGrid(GridView Gview, string BrCode, string CustNo, string Flag)
    {
        try
        {
            if (Flag == "PD")
            {
                sql = "Select ID, CustNo, ConVert(VarChar(10), APP_Date, 121) As Date, LoanType, LoanProduct As PrdCode, LoanApply, SancitonAmount, " +
                      "(Case When Stage = 1001 Then 'UnAuthorised' When Stage = 1002 Then 'UnAuthorised' When Stage = 1003 Then 'Posted' Else 'Delete' End) As Status " +
                      "From AVS1004 Where BrCd = '" + BrCode + "' And CustNo = '" + CustNo + "' And Stage Not In (1003, 1004) "+
                      "And AppNo Is Null";
                result = conn.sBindGrid(Gview, sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int Modify(string brcd, string Ltype, string Lname, string purpose, string Lapply, string Grosssal, string netsal, string twntyfive, string bymembership, string bysal, string sanctionamt, string repaycap, string installment, string interest, string totalinst,
     string Period, string stage, string mid, string appdate, string Cno, string Memno, string appstatus)
    {
        try
        {
            sql = "update AVS1004 set BRCD='" + brcd + "',LOANPRODUCT='" + Ltype + "',LOANTYPE='" + Lname + "',LOANPURPOSE='" + purpose + "',LOANAPPLY='" + Lapply + "',GROSSSAL='" + Grosssal + "'," +
                  "NETSAL='" + netsal + "',TWENTYFIVE='" + twntyfive + "',LOANELGIBLITYMEMBER='" + bymembership + "',LOANELGIBLITYSALARY='" + bysal + "',SANCITONAMOUNT='" + Lapply + "'," +
                  "REPAYCAP='" + repaycap + "',INSTMANUAL='" + installment + "',INTEREST='" + interest + "',TOTINT= '" + totalinst + "',STAGE='" + stage + "',MID= '" + mid + "',APP_DATE='" + conn.ConvertDate(appdate).ToString() + "'," +
                  "CUSTNO='" + Cno + "',MEMNO= '" + Memno + "',APPSTATUS= '" + appstatus + "', PERIOD = '" + Period + "' " +
                  "where BRCD='" + brcd + "' and LOANPRODUCT='" + Ltype + "' and CUSTNO='" + Cno + "' and stage in (1001,1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetCustMod(string brcd, string srno, string custno, string Scust, string memno, string name, string memdate, string retiredate, string loanbal, string Remser, string stands, string stage, string mid, string prd, string appstatus)
    {
        try
        {
            sql = "Update AVS1003 Set BRCD = '" + brcd + "', SURITYSRNO = '" + srno + "', MEM_NO_SURITY = '" + memno + "', SURITYNAME = '" + name + "'," +
                  "MEMBERDATE = '" + conn.ConvertDate(memdate).ToString() + "', DATEOFRET = '" + conn.ConvertDate(retiredate).ToString() + "', "+
                  "BAOFSURITY = '" + loanbal + "', REMSERVICE = '" + Remser + "', STANDS = '" + stands + "', STAGE= '" + stage + "', MID = '" + mid + "' "+
                  "Where BRCD = '" + brcd + "' And PRODUCT = '" + prd + "' And MEM_NO_SURITY = '" + Scust + "' And stage in (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetPrevMod(string brcd, string prd, string AccNo, string intrst, string deductn, string custno)
    {
        try
        {
            //  for Principle Modify
            sql = "Update AVS1005 Set Credit = '" + deductn + "' Where BRCD = '" + brcd + "' And CUSTNO = '" + custno + "' " +
                  "And PRODUCT = '" + prd + "' And ACCNO = '" + AccNo + "' And Stage in (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);

            //  for Interest Modify
            sql = "Update AVS1005 Set Credit = '" + intrst + "' Where BRCD = '" + brcd + "' And CUSTNO = '" + custno + "' " +
                  "And PRODUCT = '" + (1800 + Convert.ToInt32(prd)) + "' And ACCNO = '" + AccNo + "' And Stage in (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetStMod(string brcd, string srno, string prd, string deductn, string Percent, string mid, string dedtype, string custno)
    {
        try
        {
            sql = "Update AVS1005 Set SRNO = '" + srno + "', PRODUCT = '" + prd + "', CREDIT = '" + deductn + "', " +
                  "Percentage = '" + Percent + "', MID = '" + mid + "', DEDTYPE = '" + dedtype + "' " +
                  "Where BRCD='" + brcd + "' And PRODUCT='" + prd + "' And CUSTNO='" + custno + "' And stage In (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetOtherMod(string brcd, string srno, string prd, string deductn, string stage, string mid, string dedtype, string appstatus, string custno)
    {
        try
        {
            sql = "update  AVS1005 set BRCD='" + brcd + "',SRNO='" + srno + "',PRODUCT='" + prd + "', CREDIT='" + deductn + "',STAGE='" + stage + "',MID='" + mid + "',DEDTYPE='" + dedtype + "'," +
                "APPSTATUS='" + appstatus + "',CUSTNO='" + custno + "' where BRCD='" + brcd + "' and PRODUCT='" + prd + "' and CUSTNO='" + custno + "' and stage in (1001,1002)";

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public void AuthoLoan(string BRCD, string Prd, string CustNo, string AppNo, string BondNo)
    {

        try
        {
            sql = "update avs1005 set stage=1002,Appstatus=2,Appno='" + AppNo + "', BondNo='" + BondNo + "' where custno='" + CustNo + "' and product='" + Prd + "' and brcd='" + BRCD + "'  and stage in (1001,1002) and APPSTATUS!=4";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public int GetCustAut(string brcd, string custno, string mid, string prd, string appstatus, string appno, string bondno, string Member)
    {
        try
        {
            sql = "Update AVS1003 Set stage=1002 ,MID='" + mid + "' ,APPSTATUS= '" + appstatus + "',AppNo='" + appno + "',BONDNO='" + bondno + "' " +
                  "Where BRCD='" + brcd + "' and PRODUCT='" + prd + "' and CUSTNO='" + custno + "' and stage in (1001,1002) " +
                  "and ACCNO='" + Member + "' And APPSTATUS = '1' And AppNo Is Null ";

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Authorise(string brcd, string Ltype, string mid, string Cno, string appstatus, string appno, string bondno)
    {
        try
        {
            sql = "Update AVS1004 Set Stage = 1002, MID = '" + mid + "', APPSTATUS =  '" + appstatus + "', AppNo = '" + appno + "', BONDNO = '" + bondno + "' "+
                  "Where BRCD='" + brcd + "' And LOANPRODUCT='" + Ltype + "' And CUSTNO='" + Cno + "' And stage in (1001, 1002) "+
                  "And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetPrevAut(string brcd, string custno, string mid, string prd, string appstatus, string appno, string bondno, string LoanType, string TransType)
    {
        try
        {
            sql = "Update AVS1005 Set stage=1002, MID='" + mid + "', APPSTATUS= '" + appstatus + "', AppNo='" + appno + "', BONDNO='" + bondno + "' "+
                  "Where BRCD='" + brcd + "' And CUSTNO='" + custno + "' And stage in (1001, 1002) " +
                  "And LoanType='" + LoanType + "' And TransType=" + TransType + " And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetStAut(string brcd, string custno, string mid, string prd, string appstatus, string appno, string bondno, string LoanType, string TransType)
    {
        try
        {
            sql = "Update AVS1005 Set stage=1002, MID='" + mid + "', APPSTATUS= '" + appstatus + "', AppNo='" + appno + "', BONDNO='" + bondno + "' " +
                  "Where BRCD='" + brcd + "' And PRODUCT='" + prd + "' And CUSTNO='" + custno + "' And stage in (1001, 1002) " +
                  "And LoanType='" + LoanType + "' And TransType=" + TransType + " And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetOtherAut(string brcd, string prd, string mid, string appstatus, string custno, string appno, string bondno, string LoanType, string TransType)
    {
        try
        {
            sql = "update AVS1005 set stage=1002 ,MID='" + mid + "' ,APPSTATUS= '" + appstatus + "',AppNo='" + appno + "',BONDNO='" + bondno + "' "+
                  "where BRCD='" + brcd + "' and PRODUCT='" + prd + "' and CUSTNO='" + custno + "' and stage in (1001,1002) "+
                  "and LoanType='" + LoanType + "' and TransType=" + TransType + "";

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Delete(string brcd, string Ltype, string mid, string Cno, string appstatus)
    {
        try
        {
            sql = "delete AVS1004 where BRCD='" + brcd + "' and LOANPRODUCT='" + Ltype + "' and CUSTNO='" + Cno + "' "+
                  "And stage in (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetCustDel(string brcd, string custno, string mid, string prd, string appstatus)
    {
        try
        {
            sql = "delete AVS1003 where BRCD='" + brcd + "' and PRODUCT='" + prd + "' and CUSTNO='" + custno + "' "+
                  "And stage in (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }

    public int GetPrevDel(string brcd, string custno, string mid, string appstatus, string LoanType)
    {
        try
        {
            sql = "delete AVS1005 where BRCD='" + brcd + "' and CUSTNO ='" + custno + "' And LoanType='" + LoanType + "' "+
                  "And stage in (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetStDel(string brcd, string custno, string mid, string prd, string appstatus, string LoanType)
    {
        try
        {
            sql = "update AVS1005 set stage=1004 ,MID='" + mid + "' ,APPSTATUS= '" + appstatus + "' "+
                  "where BRCD='" + brcd + "' and PRODUCT='" + prd + "' and CUSTNO='" + custno + "' and stage in (1001, 1002) and LoanType='" + LoanType + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetOtherDel(string brcd, string prd, string mid, string appstatus, string custno, string LoanType)
    {
        try
        {
            sql = "update AVS1005 set stage=1004 ,MID='" + mid + "' ,APPSTATUS= '" + appstatus + "' "+
                  "where BRCD='" + brcd + "' and PRODUCT='" + prd + "' and CUSTNO='" + custno + "' And LoanType='" + LoanType + "' "+
                  "And stage in (1001, 1002) And APPSTATUS = '1' And AppNo Is Null ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetLoanInfo(string brcd, string id, string Ltype, string Cno)
    {
        try
        {
            sql = "Select Id, LOANPRODUCT, LOANTYPE, LOANPURPOSE, LOANAPPLY, GROSSSAL, NETSAL, TWENTYFIVE, LOANELGIBLITYMEMBER, LOANELGIBLITYSALARY, SANCITONAMOUNT, REPAYCAP, "+
                  "INSTMANUAL, INTEREST, TOTINT Where  BRCD='" + brcd + "'  and ID='" + id + "' and LOANPRODUCT='" + Ltype + "' and CUSTNO='" + Cno + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetPreviousloaninfo(string brcd, string Cno)
    {
        try
        {
            sql = "Select  L.LOANGLCODE,G.GLNAME from loaninfo L inner join glmast G on G.brcd=L.brcd and G.subglcode=L.LOANGLCODE " +
                  "where L.lmstatus=1  and L.brcd='" + brcd + "' and L.custno='" + Cno + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetCustomerDetails(string CustNo)
    {
        string Name = "";
        try
        {
            sql = "select distinct CUSTNAME+'_'+convert(nvarchar(10),m.custNo) as Name from avs_acc ac inner join Master m on ac.CUSTNO=m.CUSTNO where ac.subglcode=4 and ac.Accno='" + CustNo + "' and m.stage<>1004 and ac.ACC_STATUS<>3 ";
            Name = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Name;
    }

    public string GetClosing(string PrdCode, string CustNo, string EDAT)
    {
        string[] Date = EDAT.Split('/');
        string Month = "";
        if (Convert.ToInt32(Date[1].ToString()) < 10)
            Month = '0' + Date[1].ToString();
        else
            Month = Date[1].ToString();
        string Closing = "";
        try
        {
            sql = "SELECT isnull(abs(SUM(CASE WHEN TRXTYPE = '2' THEN -1 * isnull(AMOUNT,0) ELSE isnull(AMOUNT,0) END)),0) as Bal "+
                  "FROM avs_acc ac left join avsb_" + Date[2].ToString() + Date[1].ToString() + " b on ac.ACCNO=b.ACCNO and ac.BRCD=b.BRCD "+
                  "WHERE  ac.SUBGLCODE='" + PrdCode + "' and ac.CUSTNO='" + CustNo + "'";
            Closing = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Closing;
    }

    public DataTable GetAllDetails(string Prod, string BRCD, string CustNo)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from AVS1004 where brcd='" + BRCD + "' and custno='" + CustNo + "' and LOANPRODUCT='" + Prod + "' And STAGE In (1001, 1002) And AppNo Is Null";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable GetSurityDetails(string PROD, string CustNo, string BRCD)
    {
        try
        {
            sql = "Select MEM_NO_SURITY As Txtmemno, ACCNO As Txtcustno, SURITYNAME As Txtname, ConVert(VarChar(10), Memberdate, 121) As Memberdate, " +
                  "ConVert(VarChar(10), DATEOFRET, 121) As DATEOFRET, BAOFSURITY As LoanBal, REMSERVICE, STANDS " +
                  "From AVS1003 where brcd = '" + BRCD + "' And PRODUCT = '" + PROD + "' And CUSTNO = '" + CustNo + "' And STAGE In (1001, 1002) And AppNo Is Null " +
                  "And AppNo Is Null Order By SURITYSRNO ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public DataTable GetPrevLoan(string PROD, string CustNo, string BRCD, string Type)
    {
        try
        {
            if (Type == "1")
                sql = "Select B.Product As SubGlCode, G.GlName, B.AccNo, B.Bal As ODAMT, B.CREDIT As Amount1, A.CREDIT As Amount From AVS1005 A Inner Join GlMast G On A.BrCd = G.Brcd And A.PRODUCT = G.IR Inner Join AVS1005 B On A.BrCd = B.Brcd And A.CustNo = B.Custno And A.Accno = B.Accno And B.PRODUCT = G.SubGlCode And A.Srno = B.SrNo And B.AppNo Is Null Where A.BrCd = '" + BRCD + "' And A.CustNo = '" + CustNo + "' And A.TransType = '1' And A.AppNo Is Null And A.Stage In (1001, 1002) And A.Stage In (1001, 1002) And B.LoanType = '" + PROD + "' And B.LoanType = '" + PROD + "' And G.GlCode In (3, 11) ";
            else if (Type == "2")
                sql = "Select PRODUCT As TxtSprcd, CHQPRINTNAME As TxtSname, CREDIT As TxtSDeduction, percentage As txtPer, accno From AVS1005 Where BRCD = '" + BRCD + "' and CUSTNO = '" + CustNo + "' and TransType = 2 and STAGE in (1001,1002) and LoanType = '" + PROD + "' And AppNo Is Null";
            else if (Type == "3")
                sql = "Select PRODUCT As TxtSprcd, CHQPRINTNAME As TxtOname, CREDIT As TxtODeduction, accno From AVS1005 Where BRCD = '" + BRCD + "' and CUSTNO = '" + CustNo + "' and TransType = 3 and STAGE in (1001,1002) and LoanType = '" + PROD + "' And AppNo Is Null";
            else if (Type == "4")
                sql = "Select sum(isnull(CREDIT,0)) As Amount From AVS1005 Where BRCD = '" + BRCD + "' and CUSTNO = '" + CustNo + "' and STAGE in (1001,1002) and LoanType = '" + PROD + "' And AppNo Is Null";
            else if (Type == "5")
                sql = "Select B.Product As SubGlCode, G.GlName, B.AccNo, B.Bal As ODAMT, B.CREDIT As Amount1, A.CREDIT As Amount From AVS1005 A Inner Join GlMast G On A.BrCd = G.Brcd And A.PRODUCT = G.IR Inner Join AVS1005 B On A.BrCd = B.Brcd And A.CustNo = B.Custno And A.Accno = B.Accno And B.PRODUCT = G.SubGlCode And A.Srno = B.SrNo And B.AppNo Is Null Where A.BrCd = '" + BRCD + "' And A.CustNo = '" + CustNo + "' And A.TransType = '5' And A.AppNo Is Null And A.Stage In (1001, 1002) And A.Stage In (1001, 1002) And B.LoanType = '" + PROD + "' And B.LoanType = '" + PROD + "' And G.GlCode In (3, 11) ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public void GetLoanDetails(GridView grdDep, string BRCD, string Custno, string EDate, string FLAG)
    {
        try
        {
            sql = "Exec ISP_AVS0064 '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','" + FLAG + "'";
            conn.sBindGrid(grdDep, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public DataTable GetLoanDetails1(GridView grdDep, string BRCD, string Custno, string EDate, string FLAG)
    {
        try
        {
            sql = "Exec ISP_AVS0064 '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','" + FLAG + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetRetdate(string brcd, string prdno, string custno)
    {
        try
        {
            sql = "select Convert(varchar(10),DOR,103) from EMPDETAIL  where brcd='" + brcd + "'   and custno='" + custno + "' and  stage<>1004";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public DataTable GetDate(string BRCD, string PRD, string FDT, string TDT)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec ISP_AVSLOAN @BRCD='" + BRCD + "',@FDT='" + conn.ConvertDate(FDT) + "',@TDT='" + conn.ConvertDate(TDT) + "',@PRD='" + PRD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    
}