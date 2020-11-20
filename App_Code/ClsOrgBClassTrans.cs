﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsOrgBClassTrans
{
    string sql = "", sQuery = "";
    string sResult = "";
    DbConnection conn = new DbConnection();
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsOrgBClassTrans()
	{
		
	}

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName Where BrCd = '" + BrCode + "' And BrCd <> 0";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string GetAccNo(string BrCode, string PrCode)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BrCode + "' AND SUBGLCODE='" + PrCode + "' GROUP BY GLCODE,GLNAME";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
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

    public string openDay(string BRCD)
    {
        string wdt = "";
        try
        {
            sql = "Select ListValue From Parameter Where ListField = 'DayOpen' And BrCd = '" + BRCD + "' ";
            wdt = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return wdt;
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

    public string Getaccno(string BRCD, string AT)
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
        DataTable DT = new DataTable();
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

    public DataTable GetCRDR(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select Abs(IsNull(Sum(A.Credit), 0)) As CREDIT, Abs(IsNull(Sum(A.Debit), 0)) As DEBIT From( " +
                  "Select (Case When TRXTYPE = '1' Then Sum(Amount) Else '0' End) As Credit, " +
                  "(Case When TRXTYPE = '2' Then Sum(Amount) Else '0' End) As Debit From Avs_TempABBMultiTrf " +
                  "Where RefBrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' " +
                  "Group By TrxType)A ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string CheckStage(string setno, string edt, string BRCD)
    {
        try
        {
            string sql = "select Stage from ALLVCR where SETNO='" + setno + "' and EntryDate='" + conn.ConvertDate(edt) + "' and BRCD='" + BRCD + "'";
            sQuery = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return sQuery;
        }
        return sQuery;
    }

    public int DeleteSingleRecTable(string id, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From Avs_TempABBMultiTrf Where ID = '" + id + "' And RefBrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void DelAllRecTable(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From Avs_TempABBMultiTrf Where RefBrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public int InsertIntoTable(string BrCode, string RefBrCode, string OrgBrCode, string ResBrCode, string CustNo, string GlCode, string SubGlCode, string AccNo, string CustName, string Amount, string TrxType, string Activity, string PmtMode, string Perticulars, string Perticulars2, string ChkNo, string ChkDate, string EDate, string Mid)
    {
        try
        {
            sql = "Insert Into Avs_TempABBMultiTrf (BrCd, RefBrCd, OrgBrCd, ResBrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, RefId, InstNo, InstDate, EntryDate, SystemDate, Mid) " +
                  "VALUES ('" + BrCode + "','" + RefBrCode + "','" + OrgBrCode + "','" + ResBrCode + "','" + CustNo + "','" + CustName + "', '" + GlCode + "','" + SubGlCode + "','" + AccNo + "','" + Perticulars + "','" + Perticulars2 + "','" + Amount + "','" + TrxType + "','" + Activity + "','" + PmtMode + "','0','" + ChkNo + "','" + ChkDate + "', '" + conn.ConvertDate(EDate).ToString() + "', GetDate(), '" + Mid + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Getinfotable(GridView Gview, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select ID, SubGlCode, AccNo, CustName, Amount, Particulars, (Case When TRXTYPE = 1 Then 'Cr' Else 'Dr' End) As TrxType From Avs_TempABBMultiTrf Where RefBrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' Order By SystemDate Desc";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetTransDetails(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT ID, BrCd, RefBrcd, OrgBrCd, ResBrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, RefId, InstNo, Convert(VarChar(10), InstDate, 103) As InstDate, EntryDate, Mid From Avs_TempABBMultiTrf Where RefBrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double DebitAmount(string BrCode, string SGLCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT ABS(ISNULL(SUM(AMOUNT), 0)) AS DEBIT From Avs_TempABBMultiTrf Where RefBrCd = '" + BrCode + "' And SUBGLCODE = '" + SGLCode + "' AND ACCNO = '" + AccNo + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' AND TRXTYPE = '2'";
            sQuery = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sQuery);
    }

    public string CheckGlGroup(string BRCD, string PrCode)
    {
        string RC = "";
        try
        {
            string sql = "SELECT ISNULL(GLGROUP, '') FROM GLMAST WHERE BRCD = '" + BRCD + "' and SUBGLCODE='" + PrCode + "' AND GLGROUP = 'CBB' ";
            RC = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
        return RC;
    }

}