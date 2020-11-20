
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ClsTableCommPatch
/// </summary>
public class ClsTableCommPatch
{
    DataTable Dt = new DataTable();
    DataSet DS = new DataSet();
    DbConnection Conn = new DbConnection();
    string sql = "";
    int Result;
    int Res = 0;

	public ClsTableCommPatch()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertTrans( string Brcd, string Product, string Acc, string CustNo, string Limit, string ROI, string OpDate, string DueDate, string Period, string IntAmt,
                            string MatAmt, string Lien, string LienAmt, string TrfAcType, string TrfAcc, string Status, string IntPay, string LastInt, string Remark,
                            string RecSrno, string RECEIPT_NO, string PTD, string PTM, string PTY, string MID,string Stage)
    {
        try
        {

            sql = " Insert Into DEPOSITINFO_HISTORY (CUSTNO,CUSTACCNO,DEPOSITGLCODE,PRNAMT,RATEOFINT,OPENINGDATE,DUEDATE,PERIOD,INTAMT,MATURITYAMT,LIENMARK,LIENAMOUNT,LOANSUBGLCD,LOANACCNO,LOAN_DISBDATE,TRFACCTYPE,TRFSUBTYPE,TRFACCNO,LMSTATUS,STAGE,BRCD,MID,CID,VID,PCMAC,MOD_DATE,TIME,INTPAYOUT,PRINT_STATUS,PRINTBY,PRINT_DATE,PRDTYPE,RECEIPT_NO,PTD,PTM,PTY,LASTINTDATE,PREV_INTDATE,PeriodDD,REMARK,RECSRNO) " +
                  " Select CUSTNO,CUSTACCNO,DEPOSITGLCODE,PRNAMT,RATEOFINT,OPENINGDATE,DUEDATE,PERIOD,INTAMT,MATURITYAMT,LIENMARK,LIENAMOUNT,LOANSUBGLCD,LOANACCNO,LOAN_DISBDATE,TRFACCTYPE,TRFSUBTYPE,TRFACCNO,LMSTATUS,STAGE,BRCD,MID,CID,VID,PCMAC,MOD_DATE,TIME,INTPAYOUT,PRINT_STATUS,PRINTBY,PRINT_DATE,PRDTYPE,RECEIPT_NO,PTD,PTM,PTY,LASTINTDATE,PREV_INTDATE,PeriodDD,Concat('Data Modify On ',Format(Getdate(),'yyyy-MM-dd')),RECSRNO " +
                  " From DEPOSITINFO " +
                  " WHERE Brcd = '" + Brcd + "' And DepositglCode = '" + Product + "' And CustAccno = '" + Acc + "' " +
                  " And LMSTATUS = '" + Status + "' And Stage <> 1004";
            Result = Conn.sExecuteQuery(sql);

            sql = " Update DepositInfo Set CUSTNO = '" + CustNo + "', PRNAMT = '" + Limit + "', RATEOFINT = '" + ROI + "', " +
                  " OPENINGDATE = '" + Conn.ConvertDate(OpDate) + "', DUEDATE = '" + Conn.ConvertDate(DueDate) + "', PERIOD = '" + Period + "', INTAMT = '" + IntAmt + "', MATURITYAMT = '" + MatAmt + "', LIENMARK = '" + Lien + "', " +
                  " LIENAMOUNT = '" + LienAmt + "', TRFACCTYPE = '" + TrfAcType + "', TRFACCNO = '" + TrfAcc + "', LMSTATUS = '" + Status + "', INTPAYOUT = '" + IntPay + "', LASTINTDATE = '" + Conn.ConvertDate(LastInt) + "', " +
                  " REMARK = '" + Remark + "', RecSrno = '" + RecSrno + "', RECEIPT_NO = '" + RECEIPT_NO + "', PTD = '" + PTD + "', PTM = '" + PTM + "', PTY = '" + PTY + "',MID = '" + MID + "', Stage = '" + Stage + "'   " +
                  " WHERE Brcd = '" + Brcd + "' And DepositglCode = '" + Product + "' And CustAccno = '" + Acc + "' " +
                  " And LMSTATUS = '" + Status  +"' And Stage <> 1004";
            Result = Conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int InsertTrans_loan ( string Brcd, string Product, string Acc, string CustNo, string Limit, string Inst,
                                  string ROI, string Penal, string OpDate, string DueDate, string Status, string InstDT, string Period, string Bond,
                                  string LastInt, string DisYN, string EMI, string RecommAutho, string Equated,
                                  string IntFund, string PLRLink, string LnPurpose, string Remark, string MID, string stage)
    {
        try
        {

            sql = " Insert Into LoanInfo_H (CUSTNO,CUSTACCNO,LOANGLCODE,LIMIT,SANSSIONDATE,DRAW_POWER,INTRATE,INSTALLMENT,INSTDATE,PERIOD,INSTTERM,DUEDATE,PENAL,LMSTATUS,DEPOSITGLCODE,DEPOSITACCNO,DEPOSITAMOUNT,APPLINO,APPLDATE,APPLAMT,ELIGIBLE,BRCD,MID,CID,VID,PCMAC,STAGE,BONDNO,INCREBOND,MOD_DATE,TIME,LASTINTDATE,DISYN,PREV_INTDT,INSTTYPE,CASE_OF_DATE,CASE_MID,CASE_CID,REASON,EMI,PRINCIPAL,SancAutho,RecommAutho,Remark,Equated,IntFund,Frequency,GraceDInst,GraceDIntr,PLRLink,MoraPeriod,EffectDate,LoanPurpose,LastCrDate,Old_Cust_No,New_Cust_No) " +
                  " Select CUSTNO,CUSTACCNO,LOANGLCODE,LIMIT,SANSSIONDATE,DRAW_POWER,INTRATE,INSTALLMENT,INSTDATE,PERIOD,INSTTERM,DUEDATE,PENAL,LMSTATUS,DEPOSITGLCODE,DEPOSITACCNO,DEPOSITAMOUNT,APPLINO,APPLDATE,APPLAMT,ELIGIBLE,BRCD,MID,CID,VID,PCMAC,STAGE,BONDNO,INCREBOND,MOD_DATE,TIME,LASTINTDATE,DISYN,PREV_INTDT,INSTTYPE,CASE_OF_DATE,CASE_MID,CASE_CID,REASON,EMI,PRINCIPAL,SancAutho,RecommAutho,Concat('Data Modify On ',format(Getdate(),'yyyy-MM-dd')),Equated,IntFund,Frequency,GraceDInst,GraceDIntr,PLRLink,MoraPeriod,EffectDate,LoanPurpose,LastCrDate,Old_Cust_No,New_Cust_No " +
                  " From LoanInfo " +
                  " WHERE Brcd = '" + Brcd + "' And LoanGlcode = '" + Product + "' And CustAccno = '" + Acc + "' " +
                  " And LMSTATUS = '" + Status + "' And Stage <> 1004";
            Result = Conn.sExecuteQuery(sql);
            
            sql = " Update LoanInfo Set CUSTNO = '" + CustNo + "', LIMIT = '" + Limit + "', INSTALLMENT = '" + Inst + "', " +
                  " INTRATE = '" + ROI + "',PENAL = '" + Penal + "', SANSSIONDATE = '" + Conn.ConvertDate(OpDate) + "', DUEDATE = '" + Conn.ConvertDate(DueDate) + "', " +
                  " LMSTATUS = '" + Status + "', INSTDATE = '" + Conn.ConvertDate(OpDate) + "', PERIOD = '" + Period + "', BONDNO = '" + Bond + "', " +
                  " LASTINTDATE = '" + Conn.ConvertDate(LastInt) + "', DISYN = '" + DisYN + "', EMI = '" + EMI + "', RecommAutho = '" + RecommAutho + "', Equated = '" + Equated + "' , " +
                  " IntFund = '" + IntFund + "', PLRLink = '" + PLRLink + "', LoanPurpose = '" + LnPurpose + "', Remark = '" + Remark + "',MID = '" + MID + "' ,Stage = '" + stage + "' " +
                  " WHERE Brcd = '" + Brcd + "' And LoanGlcode = '" + Product + "' And CustAccno = '" + Acc + "' " +
                  " And LMSTATUS = '" + Status + "' And Stage <> 1004";
            //" And SANSSIONDATE = '" + Conn.ConvertDate(OpDate) + "' And LMSTATUS = '" + Status + "' And Stage <> 1004";
            Result = Conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertTrans_Acc(string Brcd, string Product, string Acc, string GlCode, string CustNo,
                                  string OpDate, string CloseDate, string AccStatus, string AccType, string OprType, string Dperiod,
                                  string DAmt, string LastInt, string RefAgent, string RefCust, string ShrBr, string Remark, string MID)
    {
        try
        {

            sql = " Insert Into AVS_ACC_HISTORY (AID,BRCD,GLCODE,SUBGLCODE,CUSTNO,ACCNO,OPENINGDATE,CLOSINGDATE,ACC_STATUS,STAGE,MID,CID,VID,PCMAC,SYSTEMDATE,ACC_TYPE,OPR_TYPE,MINOR_ACC,M_CUSTNO,M_OPRNAME,M_RELATION,M_DOB,FCHKB,FSMS,FRDC,FESTS,FAD,FIB,FMB,D_PERIOD,D_AMOUNT,OLDCTNO,LASTINTDT,Remark1,Remark2,Ref_Agent,Ref_custNo,SHR_BR,DDSLastIntPro,TrFrm,TrTo,TrfDate,SubAccStatus,RecSrNo) " +
                  " Select AID,BRCD,GLCODE,SUBGLCODE,CUSTNO,ACCNO,OPENINGDATE,CLOSINGDATE,ACC_STATUS,STAGE,MID,CID,VID,PCMAC,SYSTEMDATE,ACC_TYPE,OPR_TYPE,MINOR_ACC,M_CUSTNO,M_OPRNAME,M_RELATION,M_DOB,FCHKB,FSMS,FRDC,FESTS,FAD,FIB,FMB,D_PERIOD,D_AMOUNT,OLDCTNO,LASTINTDT,Concat('Data Modify On ',Getdate()) As Remark1,Remark2,Ref_Agent,Ref_custNo,SHR_BR,DDSLastIntPro,TrFrm,TrTo,TrfDate,SubAccStatus,RecSrNo " +
                  " From AVS_ACC " +
                  " WHERE Brcd = '" + Brcd + "' And SubGlcode = '" + Product + "' And Accno = '" + Acc + "' " +
                  " And Stage <> 1004";
            Result = Conn.sExecuteQuery(sql);

            sql = " Update Avs_Acc Set Glcode = '" + GlCode + "', CUSTNO = '" + CustNo + "', " +
                  " OPENINGDATE = '" + Conn.ConvertDate(OpDate) + "', CLOSINGDATE = '" + Conn.ConvertDate(CloseDate) + "', " +
                  " ACC_STATUS = '" + AccStatus + "', ACC_TYPE = '" + AccType + "', OPR_TYPE = '" + OprType + "', " +
                  " D_PERIOD = '" + Dperiod + "', D_AMOUNT = '" + DAmt + "', LASTINTDT = '" + Conn.ConvertDate(LastInt) + "', " +
                  " Ref_Agent = '" + RefAgent + "', Ref_custNo = '" + RefCust + "', SHR_BR = '" + ShrBr + "', Remark1 = '" + Remark + "',MID = '" + MID + "'  " +
                  " WHERE Brcd = '" + Brcd + "' And SubGlcode = '" + Product + "' And Accno = '" + Acc + "' " +
                  " And Stage <> 1004";
            Result = Conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int InsertTrans_GL(string Brcd, string Product, string GlCode, string GLName, string GLGrp,
                                  string Category, string ROI, string IntCalType, string IntApp, string IntPay,
                                  string GLBal, string PLAcc, string AccYN, string LastNo, string IR, string IOR,
                                  string IntAccYN, string ShortGLName, string PLGrp, string CashDR, string CashCR,
                                  string TrfDR, string TrfCR, string CLGDR, string CLGCR, string GLMarathi ,string ImpleDT ,string OpenBal)
    {
        try
        {

            sql = " Update LoanGl Set LOANCATEGORY = '" + Category + "', ROI = '" + ROI + "', " +
                  " INTCALTYPE = '" + IntCalType + "',LOANGLBALANCE = '" + GLBal + "', Int_App = '" + IntApp + "' " +
                  " WHERE Brcd = '" + Brcd + "' And LOANGLCODE = '" + Product + "' ";
            Result = Conn.sExecuteQuery(sql);

            sql = " Update DepositGl Set CATEGORY = '" + Category + "',DEPOSITGLBALANCE = '" + GLBal + "' , IntPay = '" + IntPay + "' " +
                  " WHERE Brcd = '" + Brcd + "' And DEPOSITGLCODE = '" + Product + "' ";
            Result = Conn.sExecuteQuery(sql);

            sql = " Update GLMast Set Glcode = '" + GlCode + "', GLName = '" + GLName + "', " +
                  " GLGROUP = '" + GLGrp + "', GLBALANCE = '" + GLBal + "', PLACCNO = '" + PLAcc + "', " +
                  " ACCNOYN = '" + AccYN + "', LASTNO = '" + LastNo + "'," +
                  " IR = '" + IR + "', IOR = '" + IOR + "'," +
                  " INTACCYN = '" + IntAccYN + "', ShortGlName = '" + ShortGLName + "'," +
                  " PLGROUP = '" + PLGrp + "', CASHDR = '" + CashDR + "'," +
                  " CASHCR = '" + CashCR + "', TRFDR = '" + TrfDR + "', TRFCR = '" + TrfCR + "', " +
                  " CLGDR = '" + CLGDR + "',CLGCR = '" + CLGCR + "',GLMarathi = '" + GLMarathi + "',ImplimentDate = '" + Conn.ConvertDate(ImpleDT) + "',OpeningBal = case when '" + OpenBal + "'='' then null else  '" + OpenBal + "' end" +
                  " WHERE Brcd = '" + Brcd + "' And SubGlcode = '" + Product + "' ";
            Result = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    
    public DataTable GetDivMemPendingListDT_2(GridView GRD, string BRCD, string Prd, string AccNo)
    {
        try
        {
            sql = "Exec Sp_TableCommPatchUpdate @Brcd='" + BRCD + "',@Product='" + Prd + "',@Accno='" + AccNo + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public int GetDepositInfo_1(GridView GRD, string BRCD, string Prd, string AccNo, string FL)
    {
        try
        {
            sql = "Exec Sp_TableCommPatchUpdate @Brcd='" + BRCD + "',@Product='" + Prd + "',@Accno='" + AccNo + "',@Flag='" + FL + "' ";
            Res = Conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int GetLoanInfo_1(GridView GRD, string BRCD, string Prd, string AccNo, string FL)
    {
        try
        {
            sql = "Exec Sp_TableCommPatchUpdate @Brcd='" + BRCD + "',@Product='" + Prd + "',@Accno='" + AccNo + "',@Flag='" + FL + "'  ";
            Res = Conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int GetGLInfo_1(GridView GRD, string BRCD, string Prd, string AccNo, string FL)
    {
        try
        {
            sql = "Exec Sp_TableCommPatchUpdate @Brcd='" + BRCD + "',@Product='" + Prd + "',@Accno='" + AccNo + "',@Flag='" + FL + "'  ";
            Res = Conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
    public int GetAccInfo_1 (GridView GRD, string BRCD, string Prd, string AccNo, string FL)
    {
        try
        {
            sql = "Exec Sp_TableCommPatchUpdate @Brcd='" + BRCD + "',@Product='" + Prd + "',@Accno='" + AccNo + "',@Flag='" + FL + "'  ";
            Res = Conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
}