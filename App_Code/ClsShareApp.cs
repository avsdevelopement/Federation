using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsShareApp
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql, SResult = "";
    string GlCode, AccNo = "";
    int Result, ShareAccNo, ShareSuspGl = 0;

	public ClsShareApp()
	{
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
	}

    public int GetShareSuspGl(string BrCode)
    {
        try
        {
            sql = "Select IsNull(SHARES_GL, 0) As SHARES_GL From AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "')";
            ShareSuspGl = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ShareSuspGl;
    }

    public int GetBClassGl(string BrCode)
    {
        try
        {
            sql = "Select IsNull(BCLASS_GL, 0) As SHARES_GL From AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "')";
            ShareSuspGl = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ShareSuspGl;
    }

    public int GetShareAccNo(string BrCode, string ShareGlCode)
    {
        try
        {
            sql = "Select Max(IsNull(A.ShareAppNo, 0)) + 1 AS AppNo From (Select Max(IsNull(LastNo, 0)) AS ShareAppNo From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + ShareGlCode + "')A";
            ShareAccNo = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ShareAccNo;
    }

    public void CreateAccounts(string BrCode, string CustNo, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select Rec_Prd From AVS_RS Where AccOpen = 'Y' And Rec_Prd <> '" + PrCode + "'";
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    GlCode = ""; AccNo = "";

                    sql = "Select GlCode From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' ";
                    GlCode = conn.sExecuteScalar(sql);

                    if (GlCode == "2")
                        sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo+1) End) LastNo From GlMast Where BrCd = '" + BrCode + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' ";
                    else
                        sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo+1) End) LastNo From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' ";

                    AccNo = conn.sExecuteScalar(sql);
                    if (Convert.ToDouble(AccNo.ToString()) > 0)
                    {
                        sql = "Insert Into Avs_Acc(BrCd, GlCode, SubGlCode, CustNo, AccNo, OpeningDate, Acc_Status, Stage, Mid, PcMac, Acc_Type, Opr_Type, SystemDate) " +
                            "Values('" + BrCode + "', '" + GlCode + "', '" + DT.Rows[i]["Rec_Prd"].ToString() + "', '" + CustNo + "', '" + AccNo + "', '" + conn.ConvertDate(EDate).ToString() + "', '1', '1001', '" + Mid + "','" + conn.PCNAME() + "', '1', '1', GetDate())";
                        Result = conn.sExecuteQuery(sql);

                        if (Result > 0)
                        {
                            if (GlCode == "2")
                                sql = "Update GlMast Set LastNo = '" + Convert.ToInt32(AccNo) + "' Where BrCd = '" + BrCode + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "'";
                            else
                                sql = "Update GlMast Set LastNo = '" + Convert.ToInt32(AccNo) + "' Where BrCd = '" + BrCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "'";
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

    public void CreateSharAccounts(string BrCode, string CustNo, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select Rec_Prd From AVS_RSACC Where AccOpen = 'Y' And Rec_Prd <> '" + PrCode + "'";
            DT = conn.GetDatatable(sql);

            string Acc = "";

            sql = "select LISTVALUE from parameter where LISTFIELD='LOANACCNO'";
            Acc = conn.sExecuteScalar(sql);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    GlCode = ""; AccNo = "";

                    sql = "Select GlCode From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' "; // Rakesh Add 12-02-2019
                    GlCode = conn.sExecuteScalar(sql);

                    if (Acc == "Y")
                    {
                        sql = "select " + CustNo + "";
                    }
                    else
                    {
                        if (GlCode == "2")
                            sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo+1) End) LastNo From GlMast Where BrCd = '" + BrCode + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' ";
                        else
                            sql = "Select (Case When LastNo Is Null Then 1 Else (LastNo+1) End) LastNo From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "' ";
                    }

                    AccNo = conn.sExecuteScalar(sql);
                    if (Convert.ToDouble(AccNo.ToString()) > 0)
                    {
                        sql = "Insert Into Avs_Acc(BrCd, GlCode, SubGlCode, CustNo, AccNo, OpeningDate, Acc_Status, Stage, Mid, PcMac, Acc_Type, Opr_Type, SystemDate) " +
                            "Values('" + BrCode + "', '" + GlCode + "', '" + DT.Rows[i]["Rec_Prd"].ToString() + "', '" + CustNo + "', '" + AccNo + "', '" + conn.ConvertDate(EDate).ToString() + "', '1', '1001', '" + Mid + "','" + conn.PCNAME() + "', '1', '1', GetDate())";
                        Result = conn.sExecuteQuery(sql);

                        if (Result > 0)
                        {
                            if (GlCode == "2")
                                sql = "Update GlMast Set LastNo = '" + Convert.ToInt32(AccNo) + "' Where BrCd = '" + BrCode + "' And GlCode = '" + GlCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "'";
                            else
                                sql = "Update GlMast Set LastNo = '" + Convert.ToInt32(AccNo) + "' Where BrCd = '" + BrCode + "' And SubGlCode = '" + DT.Rows[i]["Rec_Prd"].ToString() + "'";
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

    
    public int BindData(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT Convert(Varchar(15),Id)+'_'+Convert(Varchar(10),CustNo)+'_'+Convert(Varchar(10),AppNo) As id, BRCD, CustNo, AppNo, SHRValue, NoOfSHR, TotShrValue, "+
                  "EntFee, Other1, Other5 FROM AVS_SHRAPP WHERE BRCD = '" + BRCD + "' AND STAGE NOT IN ('1003','1004') Order By AppNo";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindAuthoData(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT Convert(Varchar(15), Id)+'_'+Convert(Varchar(10), CustNo)+'_'+Convert(Varchar(10), AppNo) As id, BRCD, CustNo, AppNo, SHRValue, NoOfSHR, TotShrValue, "+
                  "EntFee, Other1, Other2 FROM AVS_SHRAPP WHERE BRCD = '" + BRCD + "' AND STAGE <> '1004' Order By AppNo";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void BindUnallotedGrid(GridView Gview, string BrCode, string EDate)
    
    
    {
        try
        {
            sql = "Exec Sp_ShareAppllotment @BrCode = '" + BrCode + "', @CustNo = '0', @AppNo = '0', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'UAA'";

            conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public int BindDataForView(GridView Gview, string BRCD)
    {
        try
        {
            sql = "SELECT Convert(Varchar(15),Id)+'_'+Convert(Varchar(10),CustNo)+'_'+Convert(Varchar(10),AppNo) As id, BRCD, CustNo, AppNo, SHRValue, NoOfSHR, Amount, EntFee, Other1, Other2 FROM AVS_SHRAPP WHERE BRCD = '" + BRCD + "' AND STAGE = '1003'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string Getcustname(string custno, string BRCD)
    {
        try
        {
            sql = "SELECT (CUSTNAME+'_'+convert(varchar(10),Convert(int,CUSTNO))) CUSTNAME FROM MASTER WHERE CUSTNO='" + custno + "'";
            custno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }

    public string GetSavingGL(string custno, string BRCD,string PRD)
    {
        try
        {
            sql = "SELECT AccNo from avs_acc where brcd='"+BRCD+"' and subglcode='"+PRD+"' and custno='"+custno+"' and subglcode=4";
            custno = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }

    public DataTable GetAccName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT (M.CUSTNAME+'_'+convert(varchar(10),Convert(int,M.CUSTNO))) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "' AND AC.ACC_STATUS<>'3'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetAppNo(string BrCode, string Class)
    {
        try
        {
            sql = "SELECT MAX(ISNULL(A.AppNo, 0)) + 1 AS AppNo FROM (SELECT MAX(ISNULL(AppNo, 0)) AS AppNo FROM AVS_SHRAPP Where MemberClass = '" + Class + "')A";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string Id, string CustNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec Sp_ShareAppliaction @AppNo = '" + Id + "', @CustNo = '" + CustNo + "', @Flag = 'VW'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public DataTable GetShrInfo(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT SHR_VALUE, SHR_FROM, NO_OF_SHARES, EnterenceAmt, WelFareAmt, WelFareLoanAmt, SHARES_GL, ENTRY_GL, SAVING_GL, OTHERS_GL1, OTHERS_GL2 FROM AVS_SHRPARA WHERE BRCD = '" + BrCode + "' And ENTRYDATE = (SELECT MAX(ENTRYDATE) FROM AVS_SHRPARA WHERE BRCD = '" + BrCode + "')";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public int CheckMid(string Id, string CustNo)
    {
        try
        {
            sql = "SELECT MID FROM AVS_SHRAPP WHERE Id = '" + Id + "' AND CustNo = '" + CustNo + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }

    public string CheckStage(string Id, string CustNo)
    {
        try
        {
            sql = "SELECT STAGE FROM AVS_SHRAPP WHERE Id = '" + Id + "' AND CustNo = '" + CustNo + "'";
            SResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SResult;
    }

    public string SavAccNo(string Id, string CustNo)
    {
        try
        {
            sql = "SELECT SavAccNo FROM AVS_SHRAPP WHERE Id = '" + Id + "' AND CustNo = '" + CustNo + "'";
            CustNo = "";
            CustNo = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return CustNo = "";
        }
        return CustNo;
    }

    public DataTable CheckExists(string BrCode, string PrCode, string MemberNo)
    {
        try
        {
            sql = "Select A.CustNo, A.AccNo, M.CustName From AVS_Acc A "+
                  "Inner Join Master M With(NoLock) On M.CustNo = A.CustNo "+
                  "Where A.BrCd = '" + BrCode + "' And A.GlCode = '4' And A.SubGlCode = '" + PrCode + "' And A.AccNo = '" + MemberNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string getSavingname(string brcd)  //added by ashok asked by ambika mam
    {
        sql = "select glname from glmast where brcd='"+brcd+"' and subglcode=(select SAVING_GL from avs_shrpara where brcd='"+brcd+"')";
        string Name = conn.sExecuteScalar(sql);
        return Name;
    }

    public string InsertData(string BrCode, string ShareAccNo, string ApplType, string MemberNo, string CustNo, string NoOfShr, string ShrValue, string TotalShr, string EntryFee, string SavAccNo, string SavingFee, string Other1, string Other2, string Other3, string Other4, string Other5, string MemWelFee, string ServiceChrg, string Nom1, string DOB1, string Age1, string Relation1, string Nom2, string DOB2, string Age2, string Relation2, string PmtMode, string Remark, string GlCode, string SubGlCode, string AcccNo, string ChkNo, string ChkDate, string Parti, string EDate, string Mid)
    {
        string AppNo = "0";
        Result = 0;

        try
        {
            if (ApplType == "1" || ApplType == "2")
            {
                sql = "SELECT MAX(ISNULL(A.AppNo, 0)) + 1 AS AppNo FROM (SELECT MAX(ISNULL(AppNo, 0)) AS AppNo FROM AVS_SHRAPP Where MemberClass = 'A')A";
                AppNo = conn.sExecuteScalar(sql);
            }
            else if (ApplType == "3")
            {
                sql = "SELECT MAX(ISNULL(A.AppNo, 0)) + 1 AS AppNo FROM (SELECT MAX(ISNULL(AppNo, 0)) AS AppNo FROM AVS_SHRAPP Where MemberClass = 'B')A";
                AppNo = conn.sExecuteScalar(sql);
            }

          //  sql = "Exec Sp_ShareAppliaction @BRCD = '" + BrCode + "', @ShareAccNo = '" + ShareAccNo + "', @ApplType = '" + ApplType + "', @MemberNo = '" + MemberNo + "', @CustNo = '" + CustNo + "', @AppNo = '" + AppNo + "', @SHRValue = '" + ShrValue + "', @NoOfSHR = '" + NoOfShr + "', @TotSHRValue = '" + TotalShr + "', @EntFee = '" + EntryFee + "', @SavAccNo = '" + SavAccNo + "', @SavFee = '" + SavingFee + "', @Other1 = '" + Other1 + "', @Other2 = '" + Other2 + "', @Other3 = '" + Other3 + "', @Other4 = '" + Other4 + "', @Other5 = '" + Other5 + "', @MemWelFee = '" + MemWelFee + "', @SerChgFee = '" + ServiceChrg + "', @PMTMode = '" + PmtMode + "', @GlCode = '" + GlCode + "', @SubGlCode = '" + SubGlCode + "', @AccNo = '" + AcccNo + "', @InstNo = '" + ChkNo + "', @InstDate = '" + conn.ConvertDate(ChkDate).ToString() + "', @NOMI_1_NAME = '" + Nom1 + "', @NOMI_1_RALATION = '" + Relation1 + "', @NOMI_1_DOB = '" + conn.ConvertDate(DOB1).ToString() + "', @NOMI_1_AGE = '" + Age1 + "', @NOMI_2_NAME = '" + Nom2 + "', @NOMI_2_RALATION = '" + Relation2 + "', @NOMI_2_DOB = '" + conn.ConvertDate(DOB2).ToString() + "', @NOMI_2_AGE = '" + Age2 + "', @REAMARK = '" + Remark + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @Particulars = '" + Parti + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='AD'";
            sql = "Exec Sp_ShareAppliaction @BRCD = '" + BrCode + "', @ShareAccNo = '" + ShareAccNo + "', @ApplType = '" + ApplType + "', @MemberNo = '" + MemberNo + "', @CustNo = '" + CustNo + "', @AppNo = '" + AppNo + "', @SHRValue = 0, @NoOfSHR =0, @TotSHRValue = '" + ShrValue + "', @EntFee = '" + EntryFee + "', @SavAccNo = 0, @SavFee = 0, @Other1 = 0, @Other2 = 0, @Other3 = 0, @Other4 = 0, @Other5 = '" + Other5 + "', @MemWelFee = 0, @SerChgFee = 0, @PMTMode = '" + PmtMode + "', @GlCode = '" + GlCode + "', @SubGlCode = '" + SubGlCode + "', @AccNo = '" + AcccNo + "', @InstNo = '" + ChkNo + "', @InstDate = '" + conn.ConvertDate(ChkDate).ToString() + "', @NOMI_1_NAME = '" + Nom1 + "', @NOMI_1_RALATION = '" + Relation1 + "', @NOMI_1_DOB = '" + conn.ConvertDate(DOB1).ToString() + "', @NOMI_1_AGE = '" + Age1 + "', @NOMI_2_NAME = '" + Nom2 + "', @NOMI_2_RALATION = '" + Relation2 + "', @NOMI_2_DOB = '" + conn.ConvertDate(DOB2).ToString() + "', @NOMI_2_AGE = '" + Age2 + "', @REAMARK = '" + Remark + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @Particulars = '" + Parti + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return (AppNo + '_' + Result).ToString();
    }
    public string InsertData1(string BrCode, string ShareAccNo, string ApplType, string CustNo, string MemberNo, string TotalShr, string EntryFee, string SavAccNo, string Other5, string PmtMode, string GlCode, string SubGlCode, string AcccNo, string ChkNo, string ChkDate, string Parti, string EDate, string Mid)
    {
        string AppNo = "0";
        Result = 0;

        try
        {
            if (ApplType == "1" || ApplType == "2")
            {
                sql = "SELECT MAX(ISNULL(A.AppNo, 0)) + 1 AS AppNo FROM (SELECT MAX(ISNULL(AppNo, 0)) AS AppNo FROM AVS_SHRAPP Where MemberClass = 'A')A";
                AppNo = conn.sExecuteScalar(sql);
            }
            else if (ApplType == "3")
            {
                sql = "SELECT MAX(ISNULL(A.AppNo, 0)) + 1 AS AppNo FROM (SELECT MAX(ISNULL(AppNo, 0)) AS AppNo FROM AVS_SHRAPP Where MemberClass = 'B')A";
                AppNo = conn.sExecuteScalar(sql);
            }

            //  sql = "Exec Sp_ShareAppliaction @BRCD = '" + BrCode + "', @ShareAccNo = '" + ShareAccNo + "', @ApplType = '" + ApplType + "', @MemberNo = '" + MemberNo + "', @CustNo = '" + CustNo + "', @AppNo = '" + AppNo + "', @SHRValue = '" + ShrValue + "', @NoOfSHR = '" + NoOfShr + "', @TotSHRValue = '" + TotalShr + "', @EntFee = '" + EntryFee + "', @SavAccNo = '" + SavAccNo + "', @SavFee = '" + SavingFee + "', @Other1 = '" + Other1 + "', @Other2 = '" + Other2 + "', @Other3 = '" + Other3 + "', @Other4 = '" + Other4 + "', @Other5 = '" + Other5 + "', @MemWelFee = '" + MemWelFee + "', @SerChgFee = '" + ServiceChrg + "', @PMTMode = '" + PmtMode + "', @GlCode = '" + GlCode + "', @SubGlCode = '" + SubGlCode + "', @AccNo = '" + AcccNo + "', @InstNo = '" + ChkNo + "', @InstDate = '" + conn.ConvertDate(ChkDate).ToString() + "', @NOMI_1_NAME = '" + Nom1 + "', @NOMI_1_RALATION = '" + Relation1 + "', @NOMI_1_DOB = '" + conn.ConvertDate(DOB1).ToString() + "', @NOMI_1_AGE = '" + Age1 + "', @NOMI_2_NAME = '" + Nom2 + "', @NOMI_2_RALATION = '" + Relation2 + "', @NOMI_2_DOB = '" + conn.ConvertDate(DOB2).ToString() + "', @NOMI_2_AGE = '" + Age2 + "', @REAMARK = '" + Remark + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @Particulars = '" + Parti + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='AD'";
            sql = "Exec Sp_ShareAppliaction @BRCD = '" + BrCode + "', @ShareAccNo = '" + ShareAccNo + "', @ApplType = '" + ApplType + "', @MemberNo = '" + MemberNo + "', @CustNo = '" + CustNo + "', @AppNo = '" + AppNo + "', @SHRValue = 0, @NoOfSHR =0, @TotSHRValue = '" + TotalShr + "', @EntFee = '" + EntryFee + "', @SavAccNo = 0, @SavFee = 0, @Other1 = 0, @Other2 = 0, @Other3 = 0, @Other4 = 0, @Other5 = '" + Other5 + "', @MemWelFee = 0, @SerChgFee = 0, @PMTMode = '" + PmtMode + "', @GlCode = '" + GlCode + "', @SubGlCode = '" + SubGlCode + "', @AccNo = '" + AcccNo + "', @InstNo = '" + ChkNo + "', @InstDate = '" + conn.ConvertDate(ChkDate).ToString() + "', @NOMI_1_NAME = '', @NOMI_1_RALATION = '', @NOMI_1_DOB = '', @NOMI_1_AGE = '', @NOMI_2_NAME = '', @NOMI_2_RALATION = '', @NOMI_2_DOB = '', @NOMI_2_AGE = '', @REAMARK = '', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @Particulars = '" + Parti + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return (AppNo + '_' + Result).ToString();
    }

    public int ModifyData(string Id, string CustNo, string NoOfShr, string ShrValue, string TotalShr, string EntryFee, string SavingFee, string Other1, string Other2, string Nom1, string DOB1, string Age1, string Relation1, string Nom2, string DOB2, string Age2, string Relation2, string PmtMode, string Remark, string GlCode, string SubGlCode, string AcccNo, string ChkNo, string ChkDate, string EDate, string Mid)
    {
        try
        {
            sql = "Exec Sp_ShareAppliaction @CustNo = '" + CustNo + "', @AppNo = '" + Id + "', @SHRValue = '" + ShrValue + "', @NoOfSHR = '" + NoOfShr + "', @Amount = '" + TotalShr + "', @EntFee = '" + EntryFee + "', @SavFee = '" + SavingFee + "', @Other1 = '" + Other1 + "', @Other2 = '" + Other2 + "', @PMTMode = '" + PmtMode + "', @NOMI_1_NAME = '" + Nom1 + "', @NOMI_1_RALATION = '" + Relation1 + "', @NOMI_1_DOB = '" + conn.ConvertDate(DOB1).ToString() + "', @NOMI_1_AGE = '" + Age1 + "', @NOMI_2_NAME = '" + Nom2 + "', @NOMI_2_RALATION = '" + Relation2 + "', @NOMI_2_DOB = '" + conn.ConvertDate(DOB2).ToString() + "', @NOMI_2_AGE = '" + Age2 + "', @REAMARK = '" + Remark + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag='MD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthoriseData(string BrCode, string Id, string CustNo, string SetNo, string Mid)
    {
        try
        {
            sql = "Exec Sp_ShareAppliaction @BRCD = '" + BrCode + "', @CustNo = '" + CustNo + "', @AppNo = '" + Id + "', @SetNo = '" + SetNo + "', @MID = '" + Mid + "', @Flag='AT'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public int DeleteData(string Id, string CustNo, string Mid)
    {
        try
        {
            sql = "Exec Sp_ShareAppliaction @CustNo = '" + CustNo + "', @AppNo = '" + Id + "', @MID = '" + Mid + "', @Flag='DL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int CancelShareData(string Id, string CustNo, string Mid)
    {
        try
        {
            sql = "Exec Sp_ShareAppliaction @CustNo = '" + CustNo + "', @AppNo = '" + Id + "', @MID = '" + Mid + "', @Flag='CL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string SavProdCode(string BrCode)
    {
        try
        {
            sql = "Select SAVING_GL from AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (select MAX(EntryDate) from AVS_SHRPARA Where BRCD = '" + BrCode + "')";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return BrCode = "1";
        }
        return BrCode;
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

    public string GetGlName(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select GlName From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            SResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SResult;
    }

    public DataTable GetLabelName(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select LblEnterence, LblOther1, LblOther2, LblOther3, LblOther4, LblOther5, LblMemberWelLoan, LblService "+
                  "From AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "' And Stage <> 1004)";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public DataTable GetProdCode(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select * From AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "' And Stage <> 1004)";
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
            sql = "Select GLCODE from GLMAST where BRCD = '" + BrCode + "' and SUBGLCODE = '" + ProdCode + "'";
            ProdCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return ProdCode = "";
        }
        return ProdCode;
    }

    public string GetBankName(string BRCD)
    {
        try
        {
            sql = "SELECT BANKNAME FROM BANKNAME WHERE BRCD = '" + BRCD + "'";
            BRCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRCD;
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

    public int UpdateData(string Id, string CustNo, string SetNo)
    {
        try
        {
            sql = "Update AVS_SHRAPP Set SetNo = '" + SetNo + "' Where Id = '" + Id + "' And CustNo = '" + CustNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
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

    public double GetOpenClose(string Fyear, string FMonth, string PT, string ACC, string BRCD, string EDT, string GL)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OPENCLOSE @P_FLAG='CLOSING',@P_FYEAR='" + Fyear + "',@P_FMONTH='" + FMonth + "',@p_job='" + PT + "',@p_job1='" + ACC + "',@p_job2='" + BRCD + "',@p_job3='" + conn.ConvertDate(EDT) + "',@p_job4='" + GL + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public string GetRSPara1(string Subglcode)//Amruta 03/01/2018
    {
        string Result = "";
        try
        {
            sql = "select AccOpen from avs_rsAcc where REC_PRD='"+Subglcode+"'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public string GetHoPara(string Subglcode)//Amruta 03/01/2018
    {
        string Result = "";
        try
        {
            sql = "select HOEntry from avs_rsAcc where REC_PRD='" + Subglcode + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public string GetRSPara(string Subglcode, string BRCD)//Amruta 03/01/2018
    {
        string Result = "";
        try
        {
            sql = "select ACCNOYN from glmast where SUBGLCODE='" + Subglcode + "' and BRCD='" + BRCD + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }


    public string GetParameter()
    {
        string Result = "";
        try
        {
            sql = "select listvalue from PARAMETER where LISTFIELD='SHRALLOT'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public string GetAccno(string BRCD, string Subglcode, string CustNo)
    {
        string ACCNO = "";
        try
        {
            string Acc = "";

            sql = "select LISTVALUE from parameter where LISTFIELD='LOANACCNO'";
            Acc = conn.sExecuteScalar(sql);
            if (Acc == "Y")
            {
                ACCNO = "" + CustNo + "";
            }
            else
            {
                sql = "update GLMAST set LASTNO=(select isnull(convert(int,Lastno),0)+1 from glmast where brcd='" + BRCD + "' and subglcode='" + Subglcode + "') where brcd='" + BRCD + "' and subglcode='" + Subglcode + "' " +
                 " select LASTNO from GLMAST where brcd='" + BRCD + "' and subglcode='" + Subglcode + "'";
                ACCNO = conn.sExecuteScalar(sql);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ACCNO;
    }
    public string GetAccnoShr(string BRCD, string Subglcode,string CustNo)
    {
        string ACCNO = "";
        try
        {
            sql = "select top(1) ACCNO from avs_acc where subglcode='"+Subglcode+"' and brcd='"+BRCD+"' and custno='"+CustNo+"' and stage<>1004 and acc_status=1 order by accno desc";
            ACCNO = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ACCNO;
    }

    public int InsertAvsACCSHR(string BRCD,string subglcode,string glcode,string custno,string accno,string date,string MID)
    {
        try
        {
            sql = "INSERT INTO AVS_ACC (BRCD,GLCODE,SUBGLCODE,CUSTNO,ACCNO,OPENINGDATE,ACC_STATUS,STAGE,MID,SYSTEMDATE,ACC_TYPE) values " +
                "('"+BRCD+"','"+glcode+"','"+subglcode+"','"+custno+"','"+accno+"','"+conn.ConvertDate(date)+"',1,1003,'"+MID+"',getdate(),1)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
}