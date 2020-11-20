using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public class ClsAVS5040
{
    DbConnection conn = new DbConnection();
    Mobile_Service MS = new Mobile_Service();
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DataTable DT = new DataTable();
    string sResult = "", sql = "";
    int Result = 0;
    string NetPaid = "", result = "";

    public DataTable GetProdDetails(string BrCode, string PrCode)
    {
        DT = new DataTable();
        try
        {
            sql = "Select GlCode, SubGlCode, GlName, IsNull(UnOperate, '0') As UnOperate, ISNULL(IntAccYN,'N') As IntAccYN " +
                  "From GlMast With(NoLock) " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAccDetails(string BrCode, string PrCode, string AccNo)
    {
        DT = new DataTable();
        try
        {
            sql = "Select G.GlCode, G.SubGlCode, G.GlName, Ac.AccNo, Ac.RecSrNo, Ac.CustNo, M.CustName, Ac.Acc_Status, Ac.Acc_Type, Opr_Type, Minor_Acc, Ac.Stage " +
                  "From Avs_Acc Ac With(NoLock) " +
                  "Left Join GlMast G With(NoLock) On Ac.BrCd = G.BrCd And Ac.SubGlCode = G.SubGlCode " +
                  "Left Join Master M With(NoLock) On Ac.CustNo = M.CustNo " +
                  "Where Ac.BrCd = '" + BrCode + "' And Ac.SubGlCode = '" + PrCode + "' And Ac.AccNo = '" + AccNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
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

    public DataTable getClosingBal(string SetNo, string EntryDate, string Brcd, string AccNo)
    {
        try
        {
            sql = "exec SP_GetLoanAcc '" + SetNo + "','" + conn.ConvertDate(EntryDate) + "','" + Brcd + "','" + AccNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public string NetPaidProduct(string BrCode)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '" + BrCode + "' And ListField = 'Net Paid'";
            NetPaid = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return NetPaid;
    }

    public string CheckExists(string BrCode, string GlCode, string PrCode, string CustNo)
    {
        try
        {
            sql = "Select Count(1) From AVS_ACC Where BrCd = '" + BrCode + "' And GlCode = '" + GlCode + "' " +
                  "And SubGlCode = '" + PrCode + "' And CustNo = '" + CustNo + "' And AccNo = '" + CustNo + "' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }

    public int UpdateAccDetails(string BrCode, string GlCode, string PrCode, string CustNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update AVS_ACC Set OpeningDate = '" + conn.ConvertDate(EDate) + "', Mid = '" + Mid + "', Stage = '1003' " +
                  "Where BrCd = '" + BrCode + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + PrCode + "' And CustNo = '" + CustNo + "' And AccNo = '" + CustNo + "' ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public int insert(string sFlag, string BRCD, string GLCODE, string SUBGLCODE, string CUSTNO, string OPENINGDATE, string MID, string ACC_TYPE, string OPR_TYPE, string MINOR_ACC, string Stage)
    {
        try
        {
            if (Convert.ToString(sFlag) == "N")
                sql = "select " + CUSTNO + "";
            else
                sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo + 1) End) As LastNo From GlMast Where BrCd = '" + BRCD + "' AND SubGlCode = '" + SUBGLCODE + "' ";
            sResult = conn.sExecuteScalar(sql);

            if (Convert.ToString(sResult) != "")
            {
                sql = "INSERT INTO AVS_ACC(BRCD, GLCODE, SUBGLCODE, CUSTNO, ACCNO, OPENINGDATE, ACC_STATUS, ACC_TYPE, OPR_TYPE, MINOR_ACC, MID, STAGE, SYSTEMDATE) " +
                    "VALUES('" + BRCD + "', '" + GLCODE + "', '" + SUBGLCODE + "', '" + CUSTNO + "', '" + sResult + "', '" + conn.ConvertDate(OPENINGDATE).ToString() + "', " +
                    "'1', '" + ACC_TYPE + "', '" + OPR_TYPE + "', '" + MINOR_ACC + "', '" + MID + "', '" + Stage + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update GlMast Set LastNo = '" + sResult + "' Where BrCd = '" + BRCD + "' And SubGlCode = '" + SUBGLCODE + "'";
                    int RC = conn.sExecuteQuery(sql);
                }
            }
            Result = Convert.ToInt32(sResult);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpdateAcc(string Bond, string PrdCD, string cust, string BRCD, string Accno)
    {
        int i = 0;
        try
        {
            sql = "update Avs1005 set accno='" + Accno + "' where CUSTNO='" + cust + "' and brcd='" + BRCD + "' and BONDNO='" + Bond + "' and LoanType='" + PrdCD + "' and stage=1002 and accno=9999";
            i = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return i;
    }

    public DataTable GetData(string BrCode, string PrCode, string AppNo)
    {
        try
        {
            sql = "Select A.BrCd, A.LoanProduct, A.LoanType, A.AppNo, Convert(VarChar(10), A.App_Date, 103) As App_Date, A.CustNo, A.MemNo, A.SancitonAmount, A.BondNo, A.PERIOD, A.InstType, " +
                  "A.INSTMANUAL, Convert(VarChar(10), A.InstDate, 103) As InstDate, A.TOTINT " +
                  "From AVS1004 A With(NoLock) " +
                  "Where A.BrCd = '" + BrCode + "' And A.LoanProduct = '" + PrCode + "' And A.AppNo = '" + AppNo + "' And A.Stage <> '1004' " +
                  "Order By A.ID Desc ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public DataTable GetLoanData(string BrCode, string CustNo, string AppNo)
    {
        try
        {
            sql = "Select distinct A.BrCd, A.CustNo, IsNull(L1.IntCalType, '1') As IntCalType, IsNull(G1.PLACCNO, '0') As PlAccNo, G.GlCode, A.Product SubGlCode,A.AccNo as CustACCNo, M.CustName, " +
                  "(Case When IsNull(A.CREDIT, '0') = '0' Then IsNull(A.DEBIT, '0') When IsNull(A.DEBIT, '0') = '0' Then IsNull(A.CREDIT, '0') End) As Amount, " +
                  "(Case When IsNull(A.CREDIT, '0') <> '0' Then '1' When IsNull(A.DEBIT, '0') <> '0' Then '2' End) As Trxtype From AVS1005 A " +
                  "Left Join LoanGl L1 With(NoLock) On L1.BrCd = A.BrCd And L1.LoanGlCode = A.Product " +
                  "left Join LoanInfo L2 With(NoLock) On L2.BrCd = A.BrCd And L2.CustNo = A.CustNo And L2.AppliNo = A.AppNo " + //And L2.LoanGlCode = A.Product
                  "Left Join GlMast G With(NoLock) On G.BrCd = A.BrCd And G.SubGlCode = A.Product  " +
                  "Left Join GlMast G1 With(NoLock) On G1.BrCd = A.BrCd And G1.SubGlCode = A.LoanType  " +
                  "Left Join Master M with(NoLock) On M.BrCd = A.BrCd And M.CustNo = A.CustNo " +
                  "Where A.BrCd = '" + BrCode + "' And A.CustNo = '" + CustNo + "' And A.AppNo = '" + AppNo + "' and (Case When IsNull(A.CREDIT, '0') = '0' Then IsNull(A.DEBIT, '0') When IsNull(A.DEBIT, '0') = '0' Then IsNull(A.CREDIT, '0') End)<>0  Order By IntCalType Desc";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetDeductAmt(string BrCode, string CustNo, string AppNo)
    {
        try
        {
            sql = "Select IsNull(Sum(CREDIT), 0) As CREDIT From AVS1005 Where BrCd = '" + BrCode + "' And CustNo = '" + CustNo + "' And AppNo = '" + AppNo + "' And Stage <> '1004'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public DataTable GetSurityDetails(string BrCode, string PrCode, string AppNo)
    {
        try
        {
            sql = "Select BRCD,SURITYSRNO,CUSTNO,MEM_NO_SURITY,SURITYNAME,MEMBERDATE,DATEOFRET From AVS1003 " +
                  "Where BrCd = '" + BrCode + "' And Product = '" + PrCode + "' And AppNo = '" + AppNo + "' And Stage <> '1004'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public string GetSurityAccNo(string brcd, string Custno)
    {
        string AccNo = "";
        try
        {
            sql = "SELECT ACCNO FROM AVS_ACC WHERE BRCD='" + brcd + "' AND CUSTNO='" + Custno + "' AND GLCODE='3' and SUBGLCODE=201";
            AccNo = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return AccNo;
    }

    public int DeleteSurity(string BRCD, string PRD, string CustNo)
    {
        int Result = 0;
        try
        {
            sql = "delete from ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetCustNo(string BRCD, string SetNo, string EntryDate)
    {
        string CustNo = "";
        string[] DATE = EntryDate.ToString().Split('/');
        string Table = "AVSM_" + DATE[2].ToString() + DATE[1].ToString();
        try
        {
            sql = "select replace(Convert(nvarchar(100),custno),'.00','') from " + Table + " where brcd='" + BRCD + "' and SetNo='" + SetNo + "' and Entrydate='" + conn.ConvertDate(EntryDate) + "'";
            CustNo = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return CustNo;
    }

    public DataTable GetLoanDetails(string BRCD, string Custno, string Prod)
    {
        try
        {
            sql = "Exec SP_GetLoanDetails @BRCD='" + BRCD + "', @CustNo='" + Custno + "',@Prod='" + Prod + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public DataTable getMobileNo(string CustNo, string BRCD, string Prd, string Flag)
    {
        DataTable DT = new DataTable();
        try
        {
            if (Flag == "1")
                sql = "select top(1) Mobile1 from avs_contactd where brcd='" + BRCD + "' and custno='" + CustNo + "' and stage<>1004 order by systemdate desc ";
            else
                sql = "select cd.Mobile1,m.CUstName from AVSLnSurityTable AL inner join AVS_ACC AC on ac.BRCD=1 and AC.glcode=4 and AL.MemberNo=AC.ACCNO inner join  AVS_CONTACTD as CD on CD.brcd=AC.brcd and cd.CustNo=AC.CUSTNO and CD.stage<>1004 inner join master M on M.custno=Al.CUSTNO where AL.BRCD='" + BRCD + "' and AL.SubGlCode='" + Prd + "' and AL.CustNo='" + CustNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public int InsertSMSRec(string BrCode, string PrCode, string AccNo, string Message, string CreateMid, string AuthMid, string EDate, string Mobile, string Custno)
    {
        string sQuery = "";
        int Result = 0;
        try
        {
            sql = "Select ListValue From parameter Where ListField='SMSLA'";
            sQuery = conn.sExecuteScalar(sql);
            if (sQuery == "Y")
            {
                sql = "Insert Into AVS1092 (CUSTNO, MOBILE, SMS_DATE, SMS_TYPE, SMS_DESCRIPTION, SMS_STATUS, BRCD, MID, VID, PCMAC, SYSTEMDATE) " +
                    "Values('" + Custno + "','" + Mobile + "','" + conn.ConvertDate(EDate) + "','1','" + Message + "', '1', '" + BrCode + "', '" + CreateMid + "', '" + AuthMid + "', '" + conn.PCNAME() + "', GetDate())";
                Result = conn.sExecuteQuery(sql);

                //for shoot sms to customer
                string SMS = MS.Send_SMS(Custno, EDate);
                //AddSMS_Desc(BrCode.ToString(), sCustNo.ToString(), Mid, EDate.ToString(), sMobNo.ToString(), "MOD");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable Getinfotable_All(GridView Gview, string smid, string sbrcd, string EDT, string CustNo)
    {
        DataTable dt = new DataTable();
        try
        {
            string CNo = "";
            sql = "";
            string TableName = "";
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            sql = "SELECT (ConVert(VarChar(10), AM.SetNo)+'_'+ConVert(VarChar(10), AM.ScrollNo)+'_'+ConVert(VarChar(10), AM.MID)) As ScrollNo, AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " LEFT JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE AM.BRCD='" + sbrcd + "' AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + conn.ConvertDate(EDT) + "' " +
                      " And SetNo < 20000 AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32) and AM.PAYMAST='LoanAPp' and setno in (select distinct setno from " + TableName + " am WHERE AM.BRCD='" + sbrcd + "' " +
                      " AND AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + conn.ConvertDate(EDT) + "' And SetNo < 20000 AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32) and AM.PAYMAST='LoanAPp' and custno=" + CustNo + " and stage=1001)" +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable GetDepositData(string BRCD,string SubGlCode,string AccNo)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select LASTINTDATE from DepositInfo where CUSTACCNO='" + AccNo + "' and DEPOSITGLCODE='" + SubGlCode + "' and BRCD=" + BRCD + " and LMSTATUS<>'99'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int isValidDepositGL(string BRCD, string SubGlCode)
    {
        int result = 0;
        try
        {
            sql = "select top 1  isnull(GLCODE,0) GlCode From glmast where SUBGLCODE='"+SubGlCode+"' and BRCD='"+BRCD+"'";
            result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result==null?0:result;
    }
    public int CancelEntryLoan(string BrCode, string CustNo, string Prd, string SetNo, string VerifyMid, string EDate)
    {
        int result = 0;
        try
        {
            string sResult = "";
            string DeleteMid = "";
            string TableName = "";
            string AccNo = "";
            string bondNo = "";
            string AppNo = "";
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(VerifyMid);

            //  Added by amol on 14092017 for cancel loan set only
            sql = "select max(accno) from " + TableName + " where brcd='" + BrCode + "' and entrydate='" + conn.ConvertDate(EDate) + "' and setno=" + SetNo + " and stage=1001";
            AccNo = conn.sExecuteScalar(sql);

            sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + VerifyMid + "', F3 = '" + DeleteMid + "', SystemDate = GetDate() WHERE BRCD = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' and setno=" + SetNo + " and stage=1001 ";
            result = conn.sExecuteQuery(sql);

            if (result > 0)
            {
                sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + VerifyMid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' and setno=" + SetNo + " and stage=1001";
                conn.sExecuteQuery(sql);

                sql = "select distinct applino from loaninfo  where brcd='" + BrCode + "' and custno='" + CustNo + "' and CUSTACCNO='" + AccNo + "' and LOANGLCODE='" + Prd + "'";
                AppNo = conn.sExecuteScalar(sql);

                sql = "select distinct bondNo from loaninfo  where brcd='" + BrCode + "' and custno='" + CustNo + "' and CUSTACCNO='" + AccNo + "' and LOANGLCODE='" + Prd + "'";
                bondNo = conn.sExecuteScalar(sql);

                if (result > 0)
                {
                    sql = "update loaninfo set lmstatus=99, stage=1004 where brcd='" + BrCode + "' and custno='" + CustNo + "' and CUSTACCNO='" + AccNo + "' and LOANGLCODE='" + Prd + "'";
                    result = conn.sExecuteQuery(sql);
                    sql = "Update Avs_Acc Set ACC_STATUS = 3 Where BrCd = '" + BrCode + "' And SubGlCode = '" + Prd + "' And AccNo = '" + AccNo + "'";
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
    public void UpdateF7(string BondNo, string Appno, string CustNo, string Prd, string BRCD, string Setno)
    {
        try
        {
            sql = "update avs1003 set setno='" + Setno + "' where bondno=" + BondNo + " and Appno=" + Appno + " and Product='" + Prd + "' and brcd='" + BRCD + "' and custno='" + CustNo + "'";
            conn.sExecuteQuery(sql);
            sql = "update avs1004 set setno='" + Setno + "' where bondno=" + BondNo + " and Appno=" + Appno + " and LOANPRODUCT='" + Prd + "' and brcd='" + BRCD + "' and custno='" + CustNo + "'";
            conn.sExecuteQuery(sql);
            sql = "update avs1005 set setno='" + Setno + "' where bondno=" + BondNo + " and Appno=" + Appno + " and LoanType='" + Prd + "' and brcd='" + BRCD + "' and custno='" + CustNo + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public string GetGLCode(string BRCD, string subglcode)
    {
        string GLCODE = "";
        try
        {
            sql = "select glcode from glmast where brcd='" + BRCD + "' and subglcode='" + subglcode + "'";
            GLCODE = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return GLCODE;
    }

    public void OldAccClose(string BrCode, string ProdCode, string AccNo, string EDate)
    {
        double Balance = 1;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + BrCode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'MainBal'";
            Balance = Convert.ToDouble(conn.sExecuteScalar(sql));

            if (Balance == 0.00)
            {
                sql = "Update LoanInfo Set LmStatus = '99' Where BrCd = '" + BrCode + "' And LoanGlCode = '" + ProdCode + "' And CustAccNo = '" + AccNo + "' And LmStatus = '1'";
                Result = conn.sExecuteQuery(sql);
                sql = "Update Avs_Acc Set Acc_Status = '3', ClosingDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And SubGlCode = '" + ProdCode + "' And AccNo = '" + AccNo + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    

}