using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsInstruIssue
/// </summary>
public class ClsInstruIssue
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string ResStr = "", sql = "";
    string sResult = "";
    int ResInt = 0;

	public ClsInstruIssue()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     public string GetGlCode(string BrCode, string PrCode)
        {
            try
            {
                sql = "Select IsNull(GlCode, 0) As GlCode From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public string GetSubGlCode(string BrCode)
        {
            try
            {
                sql = "Select IsNull(ListValue, 0) As SubGlCode From Parameter Where BrCd = '0' And ListField = 'ServChrg'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public DataTable GetAccDetails(string BrCode, string ProdCode, string AccNo)
        {
            try
            {
                sql = "Select Ac.BrCd, Ac.Acc_Status, Ac.Acc_Type, M.CustName, Ac.OPR_TYPE, Ac.MINOR_ACC, Ac.Stage From Avs_Acc Ac With(NoLock) " +
                      "Inner Join Master M With(NoLock)On Ac.CustNo = M.CustNo " +
                      "Where Ac.BrCd = '" + BrCode + "' And Ac.SubGlCode = '" + ProdCode + "' And Ac.AccNo = '" + AccNo + "'";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public DataTable LeaveDetails(string PrCode)
        {
            try
            {
                sql = "Select GlCode, SubGlCode, Charges, FreeLeave, Status From ChargesMaster " +
                      "Where PlAcc = '" + PrCode + "' And ChargesType = 'Check Issue' And Status = '1'";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public DataTable GstDetails(string BrCode)
        {
            try
            {
                sql = "Select GST, PrdCd, CGST, CGSTPrdCd, SGST, SGSTPrdCd From GstMaster Where BrCd = '" + BrCode + "' And Stage <> 1004";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public int LeaveCount(string BrCode, string PrCode, string AccNo)
        {
            try
            {
                sql = "Select Count(1) From AVS_InstruIssueMaster Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' And Stage <> '1004'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return Convert.ToInt32(sResult);
        }

        public int Add(string FL, string Brcd, string InsType, string SNo, string BSize, string NoOfB, string EndNo, string IssueDt, string Subgl, string Accno, string Alphacd, string Remark, string Mid, string Edt)
        {
            try
            {
                sql = "Exec Isp_InstruOperation @Flag='" + FL + "',@Brcd='" + Brcd + "',@InstruType='" + InsType + "',@StartINo='" + SNo + "',@BookSize='" + BSize + "',@NoOfBooks='" + NoOfB + "',@EndINo='" + EndNo + "',@IssueDt='" + conn.ConvertDate(IssueDt) + "',@Subgl='" + Subgl + "',@Accno='" + Accno + "',@Alphacd='" + Alphacd + "',@Remark='" + Remark + "',@Mid='" + Mid + "',@Edt='" + conn.ConvertDate(Edt) + "'";
                ResInt = conn.sExecuteQuery(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return ResInt;
        }

        public string GetMaxIno(string sFlag, string BrCode)
        {
            try
            {
                sql = "Select Cast(Min(StartInsNo) As BigInt) As MaxInstruNo From AVS_InstruStockMaster " +
                       "Where BrCd = '" + BrCode + "' And InsType = '0' And Status In (0, 1) And Stage = '1003'";
                sResult = conn.sExecuteScalar(sql);

                if ((sResult == null) || (sResult.ToString() == ""))
                    sResult = "0";
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public string CheckStockExists(string BrCode, string ChequeNo)
        {
            try
            {
                sql = "Select Count(1) As InstrumentNo From AVS_InstruStockMaster Where BrCd = '" + BrCode + "' And InsType = '0' " +
                      "And StartInsNo = Right('000000' + Rtrim('" + ChequeNo + "'), 6) And Status In (0, 1) And Stage = '1003'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public string CheckStockExists(string BrCode, string FChequeNo, string TChequeNo)
        {
            try
            {
                sql = "Select Count(1) As InstrumentNo From AVS_InstruStockMaster Where BrCd = '" + BrCode + "' And InsType = '0' " +
                      "And StartInsNo Between Right('000000' + Rtrim('" + FChequeNo + "'), 6) And Right('000000' + Rtrim('" + TChequeNo + "'), 6) " +
                      "And Status In (2, 3, 4, 5, 6) And Stage = '1003' ";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return sResult;
        }

        public DataTable GetGridData(string BrCode, string SubGlCode, string AccNo)
        {
            try
            {
                sql = "Select I.BrCd, ConVert(VarChar(10), I.InstrumentDate, 103) As IssueDate, I.SubGlCode, G.GlName, I.AccNo, M.CustName, I.NoOfBook, I.BookSize, I.InsType, I.InstrumentNo, I.SplSeries, I.Remark, " +
                      "(Select Min(InstrumentNo) From AVS_InstruIssueMaster Where BrCd = '" + BrCode + "' and SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "' And Stage Not In (1003, 1004)) As SInstNo, " +
                      "(Select Max(InstrumentNo) From AVS_InstruIssueMaster Where BrCd = '" + BrCode + "' and SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "' And Stage Not In (1003, 1004)) As EInstNo, " +
                      "I.Mid, U.LoginCode As Maker From AVS_InstruIssueMaster I " +
                      "Inner Join Avs_Acc Ac With(NoLock) On I.BrCd = Ac.BrCd And I.SubGlCode = Ac.SubGlCode And I.AccNo = Ac.AccNo " +
                      "Left Join GlMast G With(NoLock) On I.BrCd = G.BrCd And I.SubGlCode = G.SubGlCode " +
                      "Left Join Master M With(NoLock) On Ac.CustNo = M.CustNo " +
                      "Left Join Usermaster U With(NoLock) On I.Mid = U.PermissionNo " +
                      "Where I.BrCd = '" + BrCode + "' And I.SubGlCode = '" + SubGlCode + "' And I.AccNo = '" + AccNo + "' And I.Stage Not In (1003, 1004) " +
                      "Order By I.BrCd, I.SubGlCode, I.AccNo, I.InstrumentNo";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public int AuthoCHQIssued(string BrCode, string SubGlCode, string AccNo, string Mid)
        {
            try
            {
                sql = "Update AVS_InstruIssueMaster Set VID = '" + Mid + "', Stage = '1003' " +
                      "Where BrCd = '" + BrCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "' And Stage Not In (1003, 1004)";
                ResInt = conn.sExecuteQuery(sql);

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return ResInt;
        }

        public int CancelCHQIssued(string BrCode, string SubGlCode, string AccNo, string Mid)
        {
            try
            {
                sql = "Update AVS_InstruIssueMaster Set CID = '" + Mid + "', Stage = '1004' " +
                      "Where BrCd = '" + BrCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "' And Stage Not In (1003, 1004)";
                ResInt = conn.sExecuteQuery(sql);

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return ResInt;
        }

        public DataTable GetCheque(string BrCode, string SubGlCode, string AccNo)
        {
            try
            {
                sql = "select Convert(varchar(11),EntryDate,103)EntryDate,Amount,SetNo,L.Description as Insttype,Convert(varchar(11),InstrumentDate,103)InstrumentDate,InstrumentNo, " +
                    " BookSize,NoOfBook,id,subglcode,brcd,accno from AVS_InstruIssueMaster C inner  join  LOOKUPFORM1 L on L.LNO = '2512' and L.Srno=C.InsType " +
                    " where C.brcd='" + BrCode + "' and C.subglcode='" + SubGlCode + "' and C.accno='" + AccNo + "' and stage='1003' and MICRprint='0' Order by InstrumentNo";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
        }

        public DataTable GetChequeSS(string BRCD, string Prd, string ACCNO)
        {
            try
            {
                sql = "EXEC ISP_AVS0189 @Brcd='" + BRCD + "',@Subgl='" + Prd + "',@Accno='" + ACCNO + "'";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return DT;
        }

        public DataTable GetChequeCPDT(string BRCD, string Prd, string ACCNO)
        {
            try
            {
                sql = "EXEC RptChequeCoverPage @Brcd='" + BRCD + "',@Subgl='" + Prd + "',@Accno='" + ACCNO + "'";
                DT = conn.GetDatatable(sql);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
            }
            return DT;
        }

        public int Chequeprintstatus(string BRCD, string Prd, string ACCNO)
        {
            try
            {
                sql = "Update AVS_InstruIssueMaster  SET MICRPRINT='1' WHERE brcd='" + BRCD + "'  and subglcode='" + Prd + "' and accno='" + ACCNO + "' and stage='1003' and MICRprint='0'";
                ResInt = conn.sExecuteQuery(sql);

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return ResInt;
        }
    }
