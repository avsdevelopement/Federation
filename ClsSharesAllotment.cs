using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsSharesAllotment
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sResult = "", sql = "", PA = "";
    string GlCode = "", AccNo = "", instDate = "";
    int Result = 0;

	public ClsSharesAllotment()
	{

	}

    public string GetShrAlloAutho()
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '0' And ListField = 'SHRALLOT'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public void BindUnallotedGrid(GridView Gview, string BrCode, string EDate)
    {
        try
        {
            sql = "Exec Sp_ShareAppllotment @BrCode = '" + BrCode + "', @CustNo = '0', @AppNo = '0', @EntryDate = '"+conn.ConvertDate(EDate).ToString()+"', @Flag = 'UA'";

            conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindAllotedGrid(GridView Gview, string BrCode, string EDate)
    {
        try
        {
            sql = "Exec Sp_ShareAppllotment @BrCode = '" + BrCode + "', @CustNo = '0', @AppNo = '0', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'AA'";

            conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string CheckAppStage(string BrCode, string CustNo, string AppNo)
    {
        try
        {
            DT = new DataTable();

            sql = "Select ApplStatus From Avs_ShrApp Where BrCd = '" + BrCode + "' And CustNo = '" + CustNo + "' And AppNo = '" + AppNo + "'";
            instDate = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return instDate;
    }

    public DataTable GetApplicationData(string BrCode, string CustNo, string AppNo)
    {
        try
        {
            DT = new DataTable();

            sql = "Exec Sp_ShareAppllotment @BrCode = '" + BrCode + "', @CustNo = '" + CustNo + "', @AppNo = '" + AppNo + "', @Flag = 'VW'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetSubGlCode(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select IsNull(SHARES_GL, 0) As SHARES_GL, IsNull(ENTRY_GL, 0) As ENTRY_GL, IsNull(OTHERS_GL1, 0) As OTHERS_GL1, IsNull(MemberWel_GL, 0) As MemberWel_GL " +
                  "From AVS_ShrPara Where BrCd = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "')";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public string GetGlCode(string BrCode, string ProdCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select GLCODE From GLMAST Where BRCD = '" + BrCode + "' And SUBGLCODE = '" + ProdCode + "'";
            ProdCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return ProdCode = "";
        }
        return ProdCode;
    }

    public DataTable GetParkAcc(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select GLCODE, SUBGLCODE From GlMast Where BrCd = '" + BrCode + "' And GlGroup = 'IBT'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public DataTable GetADMSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
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

    public string GetMemNo(string BrCode, string AccNo)
    {
        try
        {
            sql = "Select AccNo From Avs_Acc Where BrCd = '" + BrCode + "' and SubGlCode = 4 and AccNo = '" + AccNo + "'";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public string GetMaxMemNo(string BrCode)
    {
        try
        {
            sql = "Select MAX(ISNULL(A.LastNo, 0)) + 1 As LastNo From (Select ISNULL(LastNo, 0) As LastNo From GlMast "+
                  "Where BrCd = '" + BrCode + "' And GlCode = '4' And SubGlCode = '4')A";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public string GetCertNo(string BrCode)
    {
        try
        {
            sql = "Select IsNull(LISTVALUE, 1000) + 1 from Parameter Where BrCd = '" + BrCode + "' And LISTFIELD = 'CertNo'";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public string GetBoardRegNo(string BrCode)
    {
        try
        {
            sql = "Select ConVert(BigInt, Max(IsNull(A.BoardRegNo, 0))) + 1 As BoardRegNo From (Select Max(IsNull(ConVert(BigInt, BoardRegNo), 0)) As BoardRegNo From SharesInfo)A";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public string GetFromNo(string BrCode)
    {
        try
        {
            sql = "SELECT MAX(ISNULL(A.ShareTo, 0)) + 1 AS ShareTo FROM (SELECT MAX(ISNULL(ShareTo, 0)) AS ShareTo FROM SharesInfo)A";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public int UpDateStatus(string BrCode, string CustNo, string CertNo, string MemberNo, string ApplStatus, string AppNo, string MemBRCD, string EDate, string Flag)
    {
        try
        {
            sql = "Exec Sp_ShareAppllotment @BrCode = '" + BrCode + "', @CustNo = '" + CustNo + "', @CertNo = '" + CertNo + "', @MemberNo = '" + MemberNo + "', @ApplStatus = '" + ApplStatus + "', @AppNo = '" + AppNo + "', @MemBRCD = '" + MemBRCD + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetCustNo(string BRCD)
    {
        try
        {
            sql = "Select Max(ConVert(BigInt, IsNull(LastNo, 0))) + 1 From AVS1000 Where BrCd = '0' And ActivityNo = 40";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
    }

    public int InsertShrInfo(string BrCd, string CustNo, string BMeetDate, string RegNo, string AccNo, string FromNo, string ToNo, string NoOfShr, string ShrAmt, string TotShrAmt, string CertNo, string EntFeeStatus, string WelFeeStatus, string WelLoanStatus, string AppNo, string SetNo, string RefBrCd, string Stage, string Mid, string Edate, string Remark)
    {
        try
        {
            sql = "Insert Into SharesInfo (BRCD, CUSTNO, CUSTACCNO, SHAREFROM, SHARETO, TOTALSHARES, SHARESVALUE, TOTALSHAREAMT, CERT_NO, CERT_ISSUE1STDATE, BOARDMETDATE, BOARDREGNO, ENTFEESTATUS, WELFEESTATUS, WELFEELOANSTATUS, OPENDATE, APPNO, SETNO, REF_BRCD, STAGE, MID,  VID, PCMAC, MOD_DATE, REMARK, ENTRYDATE, SYSTEMDATE) " +
                  "Values ('" + BrCd + "', '" + CustNo + "', '" + AccNo + "', '" + FromNo + "', '" + ToNo + "', '" + NoOfShr + "', '" + ShrAmt + "', '" + TotShrAmt + "', '" + CertNo + "', '" + conn.ConvertDate(Edate).ToString() + "', '" + conn.ConvertDate(BMeetDate).ToString() + "', '" + RegNo + "', '" + EntFeeStatus + "', '" + WelFeeStatus + "', '" + WelLoanStatus + "', '" + conn.ConvertDate(Edate).ToString() + "', '" + AppNo + "', '" + SetNo + "', '" + RefBrCd + "', '" + Stage + "', '" + Mid + "', '" + Mid + "', '" + conn.PCNAME() + "', '" + conn.ConvertDate(Edate).ToString() + "', '" + Remark + "', '" + conn.ConvertDate(Edate).ToString() + "', GetDate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int CreateCustomer(string OldBrCode, string NewBrCode, string OldCustNo, string NewCustNo, string EDate, string Mid)
    {
        try
        {
            sql = "Exec RptCreateCustForShare @OldBrCode = '" + OldBrCode + "', @NewBrCode = '" + NewBrCode + "', @OldCustNo = '" + OldCustNo + "', @NewCustNo = '" + NewCustNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpDateStat(string BrCode, string CustNo, string AppNo, string ApplStatus, string MID, string EntryDate, string Flag)
    {
        try
        {
            sql = "Exec Sp_ShareAppllotment @BrCode = '" + BrCode + "', @CustNo = '" + CustNo + "', @ApplStatus = '" + ApplStatus + "', @AppNo = '" + AppNo + "', @MID = '" + MID + "', @PcMac = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "', @Flag = '" + Flag + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int CheckExistAcc(string custno)
    {
        try
        {
            sql = "select count(*) from avs_acc where custno='" + custno + "' and brcd=1 and glcode=4";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public string GetcustnameShare(string custno)
    {
        try
        {
            sql = "SELECT (CUSTNAME+'_'+(CUSTNAME+'_'+convert(varchar(10),Convert(bigint,CUSTNO)))) CUSTNAME FROM MASTER WHERE BrCd = '1' And CustNo = '" + custno + "'"; // And Stage In ('1001', '1002', '1003') removed ankita suggested by amol sir 20/09/17
            custno = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }

    public void CreateAccounts(string BrCode, string CustNo, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select IsNull(IntAccYN, 'N') As IntAccYN From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);

            if (sResult.ToString() == "Y")
            {
                sql = "Select GlCode From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' ";
                GlCode = conn.sExecuteScalar(sql);

                if (Convert.ToDouble(GlCode) > 0)
                {
                    sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo+1) End) LastNo From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' ";
                    AccNo = conn.sExecuteScalar(sql);

                    if (Convert.ToDouble(AccNo.ToString()) > 0)
                    {
                        sql = "Insert Into Avs_Acc(BrCd, GlCode, SubGlCode, CustNo, AccNo, OpeningDate, Acc_Status, Stage, Mid, PcMac, Acc_Type, Opr_Type, SystemDate) " +
                            "Values('" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + CustNo + "', '" + AccNo + "', '" + conn.ConvertDate(EDate).ToString() + "', '1', '1003', '" + Mid + "','" + conn.PCNAME() + "', '1', '1', GetDate())";
                        Result = conn.sExecuteQuery(sql);

                        if (Result > 0)
                        {
                            sql = "Update GlMast Set LastNo = '" + Convert.ToInt32(AccNo) + "' Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
                            Result = conn.sExecuteQuery(sql);
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}