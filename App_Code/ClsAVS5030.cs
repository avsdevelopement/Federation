using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;


    public class ClsAVS5030
    {
        ClsEncryptValue CanRes = new ClsEncryptValue();
        DbConnection conn = new DbConnection();
        DataTable DT = new DataTable();
        string sql = "", sResult = "";
        double Balance = 0;
        int Res = 0;
        string VerifyCode = "", TableName = "";
        int Result = 0;

        public ClsAVS5030()
        {

        }

        //  Added by amol on 17/05/2018
        public string GetProduct(string BrCode, string PrCode)
        {
            try
            {
                sql = "Select ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode) +'_'+ GlName From GlMast With(NoLock) " +
                      "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public string GetAccStatus(string BrCode, string PrCode, string AccNo)
        {
            try
            {
                sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + BrCode + "' AND SUBGLCODE='" + PrCode + "' AND ACCNO='" + AccNo + "' And Stage = '1003'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
        {
            DataTable DT = new DataTable();
            try
            {
                sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public DataTable GetADMSubGl(string BrCode)
        {
            DT = new DataTable();
            try
            {
                sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public DataTable GetAccInfo(string BrCode, string PrCode, string AccNo)
        {
            try
            {
                sql = "Select M.CustNo, M.CustName, Ac.GlCode, Ac.Acc_Status, (Case When Ac.Acc_Status = 1 Then 'Open' When Ac.Acc_Status = 2 Then 'In-operative' When Ac.Acc_Status = 3 Then 'CLOSED' " +
                      "When Ac.Acc_Status = 4 Then 'Lean Mark / Loan Advance' When Ac.Acc_Status = 5 Then 'Credit Freeze'  When Ac.Acc_Status = 6 Then 'Debit Freeze' " +
                      "When Ac.Acc_Status = 7 Then 'Total Freeze' When Ac.Acc_Status = 8 Then 'Dormant / In Operative' When Ac.Acc_Status = 9 Then 'Court file' " +
                      "When Ac.Acc_Status = 10 Then 'Call back Acc' When Ac.Acc_Status = 11 Then 'NPA Acc' When Ac.Acc_Status = 12 Then 'Interest Suspended' End) As AccDesc, " +
                      "Ac.MID, (Case When Ac.Mid = 999 Then 'Upload' Else U.UserName End ) As UserName, ConVert(VarChar(10), Ac.OpeningDate, 103) As OpenDate " +
                      "From Avs_Acc Ac " +
                      "Left Join Master M With(NoLock) On Ac.CustNo = M.CustNo " +
                      "Left Join UserMaster U ON Ac.MID = U.PermissionNo " +
                      "Where Ac.BrCd = '" + BrCode + "' And Ac.SubGlCode = '" + PrCode + "' And Ac.AccNo = '" + AccNo + "'";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public DataTable GetChargeHead(string BrCode, string ChrgType, string EDate)
        {
            try
            {
                sql = "Select SrNo, ChargesType, GlCode, SubGlCode, PlAcc, Status From ChargesMaster Where ChargesType = '" + ChrgType + "' " +
                      "And EffectDate = (Select Max(EffectDate) From ChargesMaster Where ChargesType = '" + ChrgType + "' And EffectDate <= '" + conn.ConvertDate(EDate) + "')";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public string CheckRates()
        {
            try
            {
                sql = "Select ListValue From Parameter Where BrCd = '0' And ListField = 'CheckRate' And Stage = '1003'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public string CheckStock(string BrCode, string PrCode, string AccNo)
        {
            try
            {
                sql = "Select Sum(BookSize) As Total From AVS_InstruStockMaster " +
                      "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' And Status = '1' ";
                sResult = conn.sExecuteScalar(sql);
                if (sResult == "") sResult = "0";
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public string UnusedCheck(string BrCode, string PrCode, string AccNo)
        {
            try
            {
                sql = "Select Count(1) As Unused From AVS_InstruIssueMaster " +
                      "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' And Status <> '1'";
                sResult = conn.sExecuteScalar(sql);
                if (sResult == "") sResult = "0";
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public double IntBalance(string BrCode, string PrCode, string AccNo, string EDate, string Mid)
        {
            try
            {
                sql = "Exec SB_AccIntCalculation @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "'";
                Balance = Convert.ToDouble(conn.sExecuteScalar(sql));
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return Balance;
        }

        public string CloseAccount(string BrCode, string PrCode, string AccNo, string EDate, string Mid)
        {
            try
            {
                sql = "Update Avs_Acc Set Acc_Status = '3', ClosingDate = '" + conn.ConvertDate(EDate) + "', Cid = '" + Mid + "' " +
                      "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public int GetEntryData(GridView GView, string BrCode, string EDate, string SetNo)
        {
            try
            {
                string[] TD = EDate.Split('/');
                string TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

                sql = "Select M.GlCode, M.SubGlCode, M.AccNo, M.Amount, (Case When TrxType = '1' Then M.Amount Else 0 End) As Credit, " +
                      "(Case When TrxType = '2' Then M.Amount Else 0 End) As Debit, M.Particulars2, U.UserName As Mid " +
                      "From " + TableName + " M With(NoLock) " +
                      "Left Join UserMaster U With(NoLock) On M.Mid = U.PermissionNo " +
                      "Where M.BrCd = '" + BrCode + "' And M.EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And M.SetNo = '" + SetNo + "' " +
                      "And M.Stage = '1001' Order By M.SystemDate ";
                conn.sBindGrid(GView, sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return Res;
        }

        public int GetEntryData_TD (GridView GView, string BrCode, string EDate, string SetNo)
        {
            try
            {
                string[] TD = EDate.Split('/');
                string TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

                sql = "Select M.Brcd, M.SubGlCode, M.AccNo, Convert(Varchar(10),M.EntryDate,103) as EntryDate, M.Amount, M.SetNo,M.ScrollNo, (Case When TrxType = '1' Then M.Amount Else 0 End) As Credit, " +
                      "(Case When TrxType = '2' Then M.Amount Else 0 End) As Debit, M.PartiCulars, U.UserName As Mid " +
                      "From " + TableName + " M With(NoLock) " +
                      "Left Join UserMaster U With(NoLock) On M.Mid = U.PermissionNo " +
                      "Where M.BrCd = '" + BrCode + "' And M.EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And M.Stage <> 1004 " +
                      "And M.PayMast = 'TDWithdrawal' And M.Glcode In (5) Order By M.SystemDate ";
                conn.sBindGrid(GView, sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return Res;
        }
        public DataTable DashboardDetails(string BrCd, string EntryDate, string Flag)
        {
            try
            {
                sql = "Exec SP_DashBoardDetails '" + BrCd + "','" + conn.ConvertDate(EntryDate).ToString() + "','" + Flag + "'";
                DT = new DataTable();
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }
        public string GetBasicPay_Month(string ListField)
        {
            try
            {
                sql = "Select ListType From Avs_loanParameter Where ListField = '" + ListField + "' ";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        // GET PLACC 
        public DataTable GetPLACC(string BrCode, string subglcode)
        {
            try
            {
                sql = "SELECT PLACCNO FROM GLMAST WHERE SUBGLCODE='" + subglcode + "' and brcd='" + BrCode + "'";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }
        public DataTable GetTDValidaation(string BRCD, string Prd, string Accno, string EDate, string Flag, string MID, string NetBal)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = "exec SP_TDWithdrwalValidation @BrCode='" + BRCD + "',@SGlCode='" + Prd + "',@AccNo='" + Accno + "',@EDate='" + conn.ConvertDate(EDate) + "',@Flag='" + Flag + "',@MID='" + MID + "',@WithdrawalAmt ='" + NetBal + "' ";
                dt = conn.GetDatatable(sql);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return dt;
        }
        public int VoucherAuthorizeCRCP(string BrCode, string EDate, string SetNo, string Mid, string ScrollNo)
        {
            try
            {
                string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
                TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

                VerifyCode = CanRes.GetCK(Mid.ToString());
                sql = "Update " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + VerifyCode + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' ";
                    conn.sExecuteQuery(sql);

                    sql = "Select Count(*) From " + TableName + " WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And Stage Not In ('1003', '1004')";
                    sResult = conn.sExecuteScalar(sql);

                    if (sResult == "1")
                    {
                        sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + VerifyCode + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                        Result = conn.sExecuteQuery(sql);

                        sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And SubGlCode = '99'";
                        conn.sExecuteQuery(sql);
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return Result;
        }
        public DataTable GetAllFieldData(string BRCD, string Prd, string Accno, string SetNo, string EDate)
        {
            try
            {
                sql = "exec RptTDVoucherGetDetils @Brcd='" + BRCD + "',@SubgL='" + Prd + "',@AccNo='" + Accno + "',@EDate='" + conn.ConvertDate(EDate) + "',@SetNo='" + SetNo + "' ";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
            }
            return DT;
        }
    }
